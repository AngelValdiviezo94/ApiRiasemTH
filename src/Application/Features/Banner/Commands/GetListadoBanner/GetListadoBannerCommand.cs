using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Banner.Dto;
using EnrolApp.Application.Features.Banner.Specifications;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Familiares.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Familiares;
using EnrolApp.Domain.Entities.Marketing;
using EnrolApp.Domain.Entities.Organizacion;
using EnrolApp.Domain.Entities.Seguridad;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EnrolApp.Application.Features.Banner.Commands.GetListadoBanner
{
    public record GetListadoBannerCommand(string Identificacion, Guid UidCanal, string TipoColaborador) : IRequest<ResponseType<List<ListadoBannerType>>>;

    public class GetListadoBannerCommandHandler : IRequestHandler<GetListadoBannerCommand, ResponseType<List<ListadoBannerType>>>
    {
        private readonly IRepositoryAsync<ColaboradorConvivencia> _repositoryCCAsync;
        private readonly IRepositoryAsync<FamiliarColaborador> _repositoryFamColAsync;
        private readonly IRepositoryAsync<RolCargoSG> _repositoryRCAsync;
        private readonly IRepositoryAsync<RolContenidoMK> _repositoryRConAsync;
        private readonly IRepositoryAsync<ContenidoCategoriaMK> _repositoryCocaAsync;
        private readonly IRepositoryAsync<CargoEje> _repositoryAsyncCargoEje;
        private readonly IConfiguration _config;
        private readonly string uidCargoFamiliar;

        public GetListadoBannerCommandHandler(IRepositoryAsync<ColaboradorConvivencia> repositoryCCAsync, IRepositoryAsync<RolCargoSG> repositoryRCAsync,
                                              IRepositoryAsync<RolContenidoMK> repositoryRConAsync, IRepositoryAsync<ContenidoCategoriaMK> repositoryCocaAsync,
                                              IRepositoryAsync<CargoEje> repositoryAsyncCargoEje, IRepositoryAsync<FamiliarColaborador> repositoryFamColAsync,
                                              IConfiguration config)
        {
            _config = config;
            _repositoryCCAsync = repositoryCCAsync;
            _repositoryRCAsync = repositoryRCAsync;
            _repositoryRConAsync = repositoryRConAsync;
            _repositoryCocaAsync = repositoryCocaAsync;
            _repositoryAsyncCargoEje = repositoryAsyncCargoEje;
            _repositoryFamColAsync = repositoryFamColAsync;
            uidCargoFamiliar = _config.GetSection("Workflow:UidCargoFamiliar").Get<string>();
        }

        public async Task<ResponseType<List<ListadoBannerType>>> Handle(GetListadoBannerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fecha = DateTime.Now;
                var lstRolContenido = new List<RolContenidoMK>();
                var lstContenidoCategoria = new List<ContenidoCategoriaMK>();
                var tipoColaborador = string.IsNullOrEmpty(request.TipoColaborador) ? string.Empty : request.TipoColaborador.ToUpper();

                string codigoCargo = string.Empty;
                string subCentroCosto = string.Empty;
                string tipoCliente = string.Empty;

                if (tipoColaborador == "F")
                {
                    var familiar = await _repositoryFamColAsync.FirstOrDefaultAsync(new GetFamiliarColaboradorByIdentificacionSpec(request.Identificacion), cancellationToken);

                    if (familiar is null)
                        return new ResponseType<List<ListadoBannerType>>() { Data = null, Message = "Familiar no existe", StatusCode = "001", Succeeded = false };

                    codigoCargo = uidCargoFamiliar;
                    tipoCliente = "FAMILIAR";
                }
                else
                {
                    var clientEje = await _repositoryAsyncCargoEje.FirstOrDefaultAsync(new GetClientesEjesSpec(string.Empty, string.Empty, string.Empty, request.Identificacion));

                    var colaboradorConvivencia = await _repositoryCCAsync.FirstOrDefaultAsync(new GetColaboradorConvivenciaByIdentificacionSpec(request.Identificacion), cancellationToken);

                    if (colaboradorConvivencia is null && clientEje is null)
                        return new ResponseType<List<ListadoBannerType>>() { Data = null, Message = "Colaborador no existe", StatusCode = "001", Succeeded = false };

                    codigoCargo = clientEje is not null ? clientEje.CargoSG.Id.ToString() : colaboradorConvivencia.CodCargo;
                    subCentroCosto = clientEje is not null ? clientEje.CargoSG.Departamento.Id.ToString() : colaboradorConvivencia.CodSubcentroCosto;
                    tipoCliente = clientEje is not null ? "EJE" : "Colaborador";
                }

                var roles = await _repositoryRCAsync.ListAsync(new GetRolesAccesoByCargoConvivenciaSpec(codigoCargo, subCentroCosto, request.UidCanal, tipoCliente), cancellationToken);

                if (!roles.Any())
                    return new ResponseType<List<ListadoBannerType>>() { Data = null, Message = "Colaborador no tiene roles asignados", StatusCode = "001", Succeeded = false };

                foreach (var rol in roles)
                {
                    var contenidos = await _repositoryRConAsync.ListAsync(new GetListadoBannerByRol(rol.RolSGId, fecha), cancellationToken);
                    lstRolContenido.AddRange(contenidos);
                }

                if (!lstRolContenido.Any())
                    return new ResponseType<List<ListadoBannerType>>() { Data = null, Message = "No tiene contenido asignado", StatusCode = "001", Succeeded = false };

                foreach (var rco in lstRolContenido)
                {
                    var contenidoCategoria = await _repositoryCocaAsync.ListAsync(new GetContenidoByIdSpec(rco.ContenidoMKId), cancellationToken);
                    lstContenidoCategoria.AddRange(contenidoCategoria);
                }

                var response = ProcesoListadoBanner(lstContenidoCategoria);

                return new ResponseType<List<ListadoBannerType>>() { Data = response, Message = CodeMessageResponse.GetMessageByCode("100"), StatusCode = "100", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<List<ListadoBannerType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

        public static List<ListadoBannerType> ProcesoListadoBanner(List<ContenidoCategoriaMK> lstContenidoCategoria)
        {
            var res = new List<ListadoBannerType>();

            foreach (var cca in lstContenidoCategoria)
            {
                if (!res.Exists(con => con.Id == cca.ContenidoId))
                    res.Add(new()
                    {
                        Id = cca.Contenido.Id,
                        TipoContenidoId = cca.Contenido.TipoContenido.Id,
                        TipoContenido = cca.Contenido.TipoContenido.Descripcion,
                        PortadaUrl = cca.Contenido.PortadaUrl,
                        PosterUrl = cca.Contenido.PosterUrl,
                        ContenidoUrl = cca.Contenido.ContenidoUrl,
                        NombreCorto = cca.Contenido.NombreCorto,
                        NombreLargo = cca.Contenido.NombreLargo,
                        Orden = cca.Contenido.Orden,
                        Descripcion = cca.Contenido.Descripcion,
                        Comentario = cca.Contenido.Comentario,
                        FechaPublicacion = cca.Contenido.FechaPublicacion,
                        FechaCaducidad = cca.Contenido.FechaCaducidad,
                        FechaVigenciaPortada = cca.Contenido.FechaVigenciaPortada,
                    });
            }

            foreach (var cca in lstContenidoCategoria)
            {
                foreach (var cat in res)
                {
                    if (cca.ContenidoId == cat.Id && !cat.ListadoCategoria.Exists(x => x.Id == cca.CategoriaId))
                        cat.ListadoCategoria.Add(new()
                        {
                            Id = cca.Categoria.Id,
                            Codigo = cca.Categoria.Codigo,
                            Descripcion = cca.Categoria.Descripcion
                        });
                }
            }

            res = res.OrderBy(x => x.Orden).ToList();

            return res;
        }
    }

}