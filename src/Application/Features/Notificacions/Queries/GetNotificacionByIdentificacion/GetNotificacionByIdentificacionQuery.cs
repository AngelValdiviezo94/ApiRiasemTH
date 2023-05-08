using Ardalis.Specification;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Common.Specifications;
using EnrolApp.Application.Features.Familiares.Specifications;
using EnrolApp.Application.Features.Notificacions.Specifications;
using EnrolApp.Application.Features.Notifications.Dto;
using EnrolApp.Application.Features.Notifications.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Familiares;
using EnrolApp.Domain.Entities.Justificacion;
using EnrolApp.Domain.Entities.Notificacion;
using EnrolApp.Domain.Entities.Permisos;
using EnrolApp.Domain.Entities.Vacaciones;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Notifications.Queries.GetProspectoByIdentificacion;

public record GetNotificacionByIdentificacionQuery(string Identificacion, string TipoColaborador) : IRequest<ResponseType<List<NotificacionResponseType>>>;

public class GetNotificacionByIdentificacionQueryHandler : IRequestHandler<GetNotificacionByIdentificacionQuery, ResponseType<List<NotificacionResponseType>>>
{
    private readonly IRepositoryAsync<BitacoraNotificacion> _repositoryAsync;
    private readonly IRepositoryAsync<Cliente> _repositoryClAsync;
    private readonly IRepositoryAsync<FamiliarColaborador> _repositoryFamColAsync;
    private readonly IRepositoryAsync<NotificacionMotivo> _repositoryNMAsync;
    private readonly IRepositoryAsync<SolicitudJustificacion> _repositoryJustiAsync;
    private readonly IRepositoryAsync<SolicitudVacacion> _repositoryVacaAsync;
    private readonly IRepositoryAsync<SolicitudPermiso> _repositoryPermiAsync;
    private readonly IRepositoryAsync<SolicitudReemplazoColaborador> _repositorySolReemColAsync;
    private readonly IRepositoryAsync<Parametros> _repositoryParamAsync;
    private readonly IConfiguration _config;
    private readonly ILogger<GetNotificacionByIdentificacionQueryHandler> _log;

    public GetNotificacionByIdentificacionQueryHandler(IRepositoryAsync<BitacoraNotificacion> repositoryAsync,
        IRepositoryAsync<Cliente> repositoryClAsync, IConfiguration config, ILogger<GetNotificacionByIdentificacionQueryHandler> log,
        IRepositoryAsync<NotificacionMotivo> repositoryNMAsync, IRepositoryAsync<SolicitudJustificacion> repositoryJustiAsync,
        IRepositoryAsync<SolicitudVacacion> repositoryVacaAsync, IRepositoryAsync<SolicitudReemplazoColaborador> repositorySolReemColAsync,
       IRepositoryAsync<SolicitudPermiso> repositoryPermiAsync, IRepositoryAsync<Parametros> repositoryParamAsync, IRepositoryAsync<FamiliarColaborador> repositoryFamColAsync)
    {
        _repositoryAsync = repositoryAsync;
        _repositoryClAsync = repositoryClAsync;
        _repositoryNMAsync = repositoryNMAsync;
        _repositoryJustiAsync = repositoryJustiAsync;
        _config = config;
        _log = log;
        _repositoryPermiAsync = repositoryPermiAsync;
        _repositoryVacaAsync = repositoryVacaAsync;
        _repositorySolReemColAsync = repositorySolReemColAsync;
        _repositoryParamAsync = repositoryParamAsync;
        _repositoryFamColAsync = repositoryFamColAsync;
    }

    public async Task<ResponseType<List<NotificacionResponseType>>> Handle(GetNotificacionByIdentificacionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<BitacoraNotificacion> resLstMotivosAprobador = new();
            List<NotificacionMotivo> lstMotivosReemplazo = new();
            List<BitacoraNotificacion> resLstNotificacionesByIdPadre = new();
            List<BitacoraNotificacion> resLstNotificacionesPropias = new();

            var diasAntiguedadNotific = _config.GetSection("Notificaciones:diasAntiguedadNotific").Get<string>();
            var uidNotificPorAprobar = Guid.Parse(_config.GetSection("Notificaciones:UidNotificPorAprobar").Get<string>());
            var codParametroReemplazo = _config.GetSection("CodigoParametro:ColaboradorReemplazo").Get<string>();

            var fechaHasta = DateTime.Now.Date;
            var fechaDesde = fechaHasta.AddDays(-Convert.ToInt32(diasAntiguedadNotific)).Date;

            var tipoColaborador = string.IsNullOrEmpty(request.TipoColaborador) ? string.Empty : request.TipoColaborador.ToUpper();

            if (tipoColaborador == "F")
            {
                var familiar = await _repositoryFamColAsync.FirstOrDefaultAsync(new GetFamiliarColaboradorByIdentificacionSpec(request.Identificacion), cancellationToken);

                // Se consultan las notificaciones propias
                resLstNotificacionesPropias = await _repositoryAsync.ListAsync(new NotificacionByIdentificacionSpec(request.Identificacion, fechaDesde, fechaHasta, "N"), cancellationToken);

                // Se excluyen las propias generadas en estado por aprobar
                resLstNotificacionesPropias = resLstNotificacionesPropias.Where(np => np.EventoDifusion.ClasificacionId != uidNotificPorAprobar).ToList();
            }
            else
            {
                var suscriptor = await _repositoryClAsync.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(request.Identificacion), cancellationToken);

                #region Modificación consulta
                // Se consultan las notificaciones propias
                resLstNotificacionesPropias = await _repositoryAsync.ListAsync(new NotificacionByIdentificacionSpec(request.Identificacion, fechaDesde, fechaHasta, "N"), cancellationToken);

                // Se excluyen las propias generadas en estado por aprobar
                resLstNotificacionesPropias = resLstNotificacionesPropias.Where(np => np.EventoDifusion.ClasificacionId != uidNotificPorAprobar).ToList();

                // Se consulta si colaborador ha solicitado reemplazo por permiso o vacación
                var colaboradorSolicitudReem = await _repositorySolReemColAsync.ListAsync(new GetColaboradorReemplazoByIdentificacionSpec(request.Identificacion, DateTime.Now, "2"), cancellationToken);

                // Se consulta paramtrización para mostrar notificaciones en caso de tener reemplazo
                var parametro = await _repositoryParamAsync.FirstOrDefaultAsync(new GetParamterosByCodigoSpec(codParametroReemplazo), cancellationToken);
                string valorParametro = parametro.Valor;

                // Si tiene un reemplazo solicitado se omiten consultas de jefe inmediato y motivos
                if (!colaboradorSolicitudReem.Any() || valorParametro == "S")
                {
                    // Se consulta la parametrizacion de motivos por colaborador aprobador
                    var lstAprobNotifiMotivo = await _repositoryNMAsync.ListAsync(new ListTipoMotivoSpec());

                    // Se consultan las notificaciones de los colaboradores que tiene a cargo 
                    resLstNotificacionesByIdPadre = await _repositoryAsync.ListAsync(new NotificacionByIdentificacionSuscriptorPadreSpec(fechaDesde, fechaHasta, suscriptor.Id, uidNotificPorAprobar, "N"), cancellationToken);

                    #region Lógica reemplazo / Se consulta en caso de ser reemplazo (colaboradores a cargo)
                    var colaReem = await _repositorySolReemColAsync.ListAsync(new GetColaboradorReemplazoByIdentificacionSpec(request.Identificacion, DateTime.Now, "1"), cancellationToken);

                    if (colaReem.Any())
                    {
                        foreach (var cr in colaReem)
                        {
                            var colaboradorPorReemplazo = await _repositoryClAsync.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(cr.IdentificacionColaborador), cancellationToken);

                            // Se consulta colaboradores a cargo por colaborador reemplazo
                            var noti = await _repositoryAsync.ListAsync(new NotificacionByIdentificacionSuscriptorPadreSpec(fechaDesde, fechaHasta, colaboradorPorReemplazo.Id, uidNotificPorAprobar, "N"), cancellationToken);
                            resLstNotificacionesByIdPadre.AddRange(noti);

                            // Se obtienen motivos de la parametrización por colaborador reemplazo 
                            var motivosReemplazo = lstAprobNotifiMotivo.Where(x => x.AprobadorId == colaboradorPorReemplazo.Id).ToList();
                            lstMotivosReemplazo.AddRange(motivosReemplazo);
                        }
                    }
                    #endregion

                    // Se obtiene id de estado "Solicitada" de la solicitud 
                    var idEstadoSolicitada = Guid.Parse(_config.GetSection("EstadosWorkflow:Solicitada").Get<string>());

                    //Se consultan las solicitudes en estado solicitadas según los motivos parametrizados
                    List<SolicitudPermiso> lstSolicitudPermiso = new();
                    List<SolicitudJustificacion> lstSolicitudJustificacion = new();

                    foreach (var mc in lstAprobNotifiMotivo)
                    {
                        if (mc.Feature.Codigo == "JUS")
                        {
                            var lstTemp = await _repositoryJustiAsync.ListAsync(new SolicitudJustificacionByMotivoSpec(mc.TipoMotivoId, fechaDesde, fechaHasta, idEstadoSolicitada));
                            if (lstTemp.Count > 0) lstSolicitudJustificacion.AddRange(lstTemp);
                        }

                        if (mc.Feature.Codigo == "PER")
                        {
                            var lstTemp = await _repositoryPermiAsync.ListAsync(new SolicitudPermisoByMotivoSpec(mc.TipoMotivoId, fechaDesde, fechaHasta, idEstadoSolicitada));
                            if (lstTemp.Count > 0) lstSolicitudPermiso.AddRange(lstTemp);
                        }
                    }

                    // Se excluyen las solicitudes (PER y JUS) idPadre que están en la parametrización
                    resLstNotificacionesByIdPadre = resLstNotificacionesByIdPadre.Where(x => !lstSolicitudPermiso.Any(lsp => lsp.Id == x.SolicitudId)).ToList();
                    resLstNotificacionesByIdPadre = resLstNotificacionesByIdPadre.Where(x => !lstSolicitudJustificacion.Any(lsp => lsp.Id == x.SolicitudId)).ToList();

                    // Se seleccionan los motivos en las que se encuentra parametrizado el colaborador
                    var lstMotivosColaborador = lstAprobNotifiMotivo.Where(x => x.AprobadorId == suscriptor.Id).ToList();

                    // Se llena lista principal motivos colaborador con motivos de colaborador reemplazo
                    if (lstMotivosReemplazo.Any())
                    {
                        lstMotivosColaborador.AddRange(lstMotivosReemplazo);
                        lstMotivosColaborador = lstMotivosColaborador.DistinctBy(x => x.Id).ToList();
                    }

                    // Se seleccionan las solicitudes según el motivo en las que se encuentre parametrizado
                    var lstPermisoAprobador = lstSolicitudPermiso.Where(x => lstMotivosColaborador.Any(mc => mc.TipoMotivoId == x.IdTipoPermiso)).ToList();
                    var lstJustificacionAprobador = lstSolicitudJustificacion.Where(x => lstMotivosColaborador.Any(mc => mc.TipoMotivoId == x.IdTipoJustificacion)).ToList();

                    //Se obtienen notificaciones por el id de la solicitud (PER y JUS)
                    foreach (var pa in lstPermisoAprobador)
                    {
                        var resNot = await _repositoryAsync.FirstOrDefaultAsync(new NotificacionByIdSolicitudSpec(pa.Id, uidNotificPorAprobar));
                        if (resNot is not null) resLstMotivosAprobador.Add(resNot);
                    }

                    foreach (var pa in lstJustificacionAprobador)
                    {
                        var resNot = await _repositoryAsync.FirstOrDefaultAsync(new NotificacionByIdSolicitudSpec(pa.Id, uidNotificPorAprobar));
                        if (resNot is not null) resLstMotivosAprobador.Add(resNot);
                    }
                }
            }

            List<BitacoraNotificacion> reslstNotificaciones = new();
            reslstNotificaciones.AddRange(resLstNotificacionesPropias);
            reslstNotificaciones.AddRange(resLstNotificacionesByIdPadre);
            reslstNotificaciones.AddRange(resLstMotivosAprobador);

            var result = (from x in reslstNotificaciones
                          orderby x.EventoDifusion.Clasificacion.Orden ascending, x.FechaCreacion descending
                          select new NotificacionResponseType()
                          {
                              Id = x.Id,
                              FeatureId = x.EventoDifusion.Evento.FeatureId,
                              EstadoLeido = x.EstadoLeido,
                              FechaCreacion = x.FechaCreacion,
                              MensajeNotificacion = x.Mensaje,
                              MensajeNotificacionHtml = x.MensajeHtml,
                              Resumen = x.Resumen ?? string.Empty,
                              UriImageNotifacion = x.EventoDifusion?.UriImage ?? string.Empty,
                              SolicitudId = x.SolicitudId,
                              TipoSolicitud = x.TipoSolicitud,
                              CodigoClasificacion = x.EventoDifusion?.Clasificacion?.Codigo ?? string.Empty,
                              DescripClasificacion = x.EventoDifusion?.Clasificacion?.Descripcion ?? string.Empty,
                              UriImageClasificacion = x.EventoDifusion?.Clasificacion?.UriImage ?? string.Empty,
                              OrdenClasificacion = x.EventoDifusion.Clasificacion.Orden,
                              RequiereAccion = x.EventoDifusion?.Plantilla?.RequiereAccion ?? false,
                              Relevante = x.EventoDifusion?.Plantilla?.Relevante == true ? "S" : "N",
                              RequiereNivelDetalle = x.EventoDifusion?.Plantilla?.RequiereNivelDetalle ?? false,
                              IdTransaccion = x.IdTransaccion,
                              IdPuntoOperacion = x.IdPuntoOperacion
                          }).ToList();
            result = result.DistinctBy(x => x.Id).ToList();
            #endregion

            return new ResponseType<List<NotificacionResponseType>>() { Data = result, Succeeded = true, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000" };
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<List<NotificacionResponseType>>() { Data = null, Succeeded = true, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500" };
        }
    }

}