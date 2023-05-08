using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Horario;
using MediatR;

namespace EnrolApp.Application.Features.Clients.Commands.UpdateInfoColaborador
{
    public record UpdateInfoColaboradorCommand(UpdateInfoColaboradorRequest Request) : IRequest<ResponseType<string>>;

    public class UpdateInfoColaboradorCommandHandler : IRequestHandler<UpdateInfoColaboradorCommand, ResponseType<string>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsyncCl;
        private readonly IRepositoryAsync<Localidad> _repositoryAsyncLoc;
        private readonly IRepositoryAsync<LocalidadColaborador> _repositoryAsyncLocCol;

        public UpdateInfoColaboradorCommandHandler(IRepositoryAsync<Cliente> repository, IRepositoryAsync<Localidad> repositoryAsyncLoc, IRepositoryAsync<LocalidadColaborador> repositoryAsyncLocCol)
        {
            _repositoryAsyncCl = repository;
            _repositoryAsyncLoc = repositoryAsyncLoc;
            _repositoryAsyncLocCol = repositoryAsyncLocCol;
        }

        public async Task<ResponseType<string>> Handle(UpdateInfoColaboradorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var req = request.Request;
                var cliente = await _repositoryAsyncCl.GetByIdAsync(req.IdColaborador);
                var locActual = await _repositoryAsyncLocCol.ListAsync(new GetLocalidadColaboradoresSpec(req.IdColaborador));

                cliente.ClientePadreId = !string.IsNullOrEmpty(req.IdJefe) ? Guid.Parse(req.IdJefe) : null;
                cliente.ColaboradorReemplazoId = !string.IsNullOrEmpty(req.IdColaboradorReemplazo) ? Guid.Parse(req.IdColaboradorReemplazo) : null;

                List<Guid> lstlocalidad = req.LstLocalidad;
                List<LocalidadColaborador> lstNewLoc = new();
                List<Guid> lstNuevaExistente = new();

                var locExistente = locActual.Where(x => lstlocalidad.Any(l => l == x.IdLocalidad)).ToList();
                var locAgregar = lstlocalidad.Where(x => !locExistente.Any(l => l.IdLocalidad == x)).ToList();

                lstNuevaExistente.AddRange(locExistente.Select(l => l.IdLocalidad).ToList());
                lstNuevaExistente.AddRange(locAgregar);

                var locInactive = locActual.Where(x => !lstNuevaExistente.Any(l => l == x.IdLocalidad)).ToList();

                await _repositoryAsyncCl.UpdateAsync(cliente);

                /* actualizar en caso de localidades inactivas */
                if (locInactive.Count > 0)
                {
                    foreach (var li in locInactive)
                    {
                        li.Estado = "I";
                        li.FechaModificacion = DateTime.Now;
                        li.UsuarioModificacion = req.IdentificacionModifica;
                        li.EsPrincipal = false;
                    }

                    await _repositoryAsyncLocCol.UpdateRangeAsync(locInactive);
                }

                /* actualizar en caso de localidades agregadas */
                if (locAgregar.Count() > 0)
                {
                    foreach (var la in locAgregar)
                    {
                        lstNewLoc.Add(new LocalidadColaborador
                        {
                            Id = Guid.NewGuid(),
                            IdLocalidad = la,
                            IdColaborador = cliente.Id,
                            Estado = "A",
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = req.IdentificacionModifica,
                            FechaModificacion = null,
                            UsuarioModificacion = string.Empty,
                            EsPrincipal = req.LocalidadPrincipal == la ? true : false,
                        });
                    }

                    await _repositoryAsyncLocCol.AddRangeAsync(lstNewLoc);
                }

                /* actualizar en caso de localidades existentes */
                if (locExistente.Count > 0)
                {
                    var locPrincipal = locExistente.FirstOrDefault(x => x.IdLocalidad == req.LocalidadPrincipal && x.EsPrincipal);

                    if (locPrincipal == null)
                    {
                        foreach (var item in locExistente)
                        {
                            if (item.EsPrincipal)
                            {
                                item.EsPrincipal = false;
                                item.UsuarioModificacion = req.IdentificacionModifica;
                                item.FechaModificacion = DateTime.Now;
                            }

                            if (item.IdLocalidad == req.LocalidadPrincipal)
                            {
                                item.EsPrincipal = true;
                                item.UsuarioModificacion = req.IdentificacionModifica;
                                item.FechaModificacion = DateTime.Now;
                            }
                        }

                        await _repositoryAsyncLocCol.UpdateRangeAsync(locExistente);
                    }
                }

                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("200"), StatusCode = "200", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }
}
