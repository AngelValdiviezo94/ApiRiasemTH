using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Horario;
using EnrolApp.Domain.Entities.Organizacion;
using EnrolApp.Domain.Entities.Suscripcion;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EnrolApp.Application.Features.Clients.Commands.GetListadoColaboradores
{
    public record GetListadoColaboradoresCommand(GetListadoColaboradoresRequest Request) : IRequest<ResponseType<List<ListadoColaboradoresType>>>;

    public class GetListadoColaboradoresCommandHandler : IRequestHandler<GetListadoColaboradoresCommand, ResponseType<List<ListadoColaboradoresType>>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsyncCl;
        private readonly IRepositoryAsync<CargoEje> _repositoryAsyncCargoEje;
        private readonly IRepositoryAsync<Localidad> _repositoryAsyncLoc;
        private readonly IRepositoryAsync<LocalidadColaborador> _repositoryAsyncLocCol;
        private readonly IRepositoryAsync<ColaboradorConvivencia> _repositoryAsyncColConv;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly string fotoPerfilDefecto;

        public GetListadoColaboradoresCommandHandler(IRepositoryAsync<Cliente> repository, IRepositoryAsync<Localidad> repositoryAsyncLoc,
                                                    IRepositoryAsync<LocalidadColaborador> repositoryAsyncLocCol,
                                                    IRepositoryAsync<ColaboradorConvivencia> repositoryAsyncColConv, IMapper mapper,
                                                    IRepositoryAsync<CargoEje> repositoryAsyncCargoEje, IConfiguration config)
        {
            _repositoryAsyncCl = repository;
            _repositoryAsyncLoc = repositoryAsyncLoc;
            _repositoryAsyncLocCol = repositoryAsyncLocCol;
            _repositoryAsyncColConv = repositoryAsyncColConv;
            _mapper = mapper;
            _repositoryAsyncCargoEje = repositoryAsyncCargoEje;
            _config = config;
            fotoPerfilDefecto = _config.GetSection("Imagenes:FotoPerfilDefecto").Get<string>();
        }

        public async Task<ResponseType<List<ListadoColaboradoresType>>> Handle(GetListadoColaboradoresCommand request, CancellationToken cancellationToken)
        {
            List<ListadoColaboradoresType> listadoColaboradores = new();
            var req = request.Request;

            try
            {
                var colConv = await _repositoryAsyncColConv.ListAsync(new GetProspectoByUdnAreaSccSpec(req.Udn, req.Area, req.Scc, req.Colaborador), cancellationToken);
                var colaboradores = await _repositoryAsyncCl.ListAsync(new GetColaboradoresSpec());
                var localidadColaborador = await _repositoryAsyncLocCol.ListAsync(new GetLocalidadColaboradoresSpec(Guid.Empty));
                var localidadMaestro = await _repositoryAsyncLoc.ListAsync(new GetLocalidadSpec());
                var clientEje = await _repositoryAsyncCargoEje.ListAsync(new GetClientesEjesSpec(req.Udn, req.Area, req.Scc, req.Colaborador));
                var lstCola = colaboradores.Where(c => colConv.Any(p => c.Identificacion == p.Identificacion) || clientEje.Any(cl => c.Identificacion == cl.Identificacion)).ToList();

                if (lstCola.Count == 0) return new ResponseType<List<ListadoColaboradoresType>>() { Data = listadoColaboradores, Message = CodeMessageResponse.GetMessageByCode("001"), StatusCode = "001", Succeeded = false };

                foreach (var item in lstCola)
                {
                    List<LocalidadColaborador> locCol = new();
                    List<Localidad> localfinal = new();
                    ColaboradorConvivencia cc = null;

                    var ce = clientEje.FirstOrDefault(x => x.Identificacion == item.Identificacion);

                    if (ce == null)
                        cc = colConv.FirstOrDefault(x => x.Identificacion == item.Identificacion);

                    locCol = localidadColaborador.Where(x => x.IdColaborador == item.Id).ToList();

                    var jefe = colaboradores.FirstOrDefault(x => x.Id == item.ClientePadreId);
                    var reemplazo = colaboradores.FirstOrDefault(x => x.Id == item.ColaboradorReemplazoId);

                    if (locCol.Any())
                    {
                        localfinal = localidadMaestro.Where(l => locCol.Any(lc => lc.IdLocalidad == l.Id)).ToList();

                        foreach (var lf in localfinal)
                        {
                            lf.EsPrincipal = locCol.FirstOrDefault(x => x.IdLocalidad == lf.Id).EsPrincipal;
                        }
                    }

                    listadoColaboradores.Add(new ListadoColaboradoresType()
                    {
                        Id = item.Id,
                        Cedula = item.Identificacion,
                        Celular = item.Celular,
                        Correo = item.Correo,
                        Codigo = item.CodigoConvivencia,
                        Colaborador = ce is not null ? colaboradores.FirstOrDefault(x => x.Identificacion == item.Identificacion).Apellidos + " " + colaboradores.FirstOrDefault(x => x.Identificacion == item.Identificacion).Nombres : cc.Empleado,/*ce.Any() ? ce.FirstOrDefault().Apellidos + " " + ce.FirstOrDefault().Nombres : cc.Empleado,*/
                        Direccion = item.Direccion,
                        CodUdn = ce is not null ? ce.CargoSG.Departamento.Area.Empresa.Codigo : cc.CodUdn,
                        Udn = ce is not null ? ce.CargoSG.Departamento.Area.Empresa.RazonSocial : cc.DesUdn,
                        CodArea = ce is not null ? ce.CargoSG.Departamento.Area.Codigo : cc.CodArea,
                        Area = ce is not null ? ce.CargoSG.Departamento.Area.Nombre : cc.DesArea,
                        CodScc = ce is not null ? ce.CargoSG.Departamento.Codigo : cc.CodSubcentroCosto,
                        Scc = ce is not null ? ce.CargoSG.Departamento.Nombre : cc.DesSubcentroCosto,
                        Cargo = ce is not null ? ce.CargoSG.Nombre : cc.DesCargo,
                        IdJefe = jefe is not null ? jefe.Id : null,
                        Jefe = jefe is not null ? string.Concat(jefe.Apellidos, " ", jefe.Nombres) : string.Empty,
                        IdReemplazo = reemplazo is not null ? reemplazo.Id : null,
                        Reemplazo = reemplazo is not null ? string.Concat(reemplazo.Apellidos, " ", reemplazo.Nombres) : string.Empty,
                        LstLocalidad = localfinal,
                        FotoPerfil = item.ImagenPerfil is not null ? item.ImagenPerfil.RutaAcceso : fotoPerfilDefecto,
                        FacialPersonId = item.FacialPersonId ?? null,
                        Latitud = item.Latitud,
                        Longitud = item.Longitud,
                    });
                }

                return new ResponseType<List<ListadoColaboradoresType>>() { Data = listadoColaboradores, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
            }
            catch (Exception ex)
            {
                return new ResponseType<List<ListadoColaboradoresType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }
}
