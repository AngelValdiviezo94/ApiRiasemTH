using Ardalis.Specification;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Notificacions.Specifications;
using EnrolApp.Application.Features.Notifications.Dto;
using EnrolApp.Application.Features.Notifications.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Justificacion;
using EnrolApp.Domain.Entities.Notificacion;
using EnrolApp.Domain.Entities.Permisos;
using EnrolApp.Domain.Entities.Vacaciones;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Notifications.Queries.GetProspectoByIdentificacion;

public record GetNotificacionByIdentificacionAppQuery(string Identificacion, bool EstadoLeido) : IRequest<ResponseType<List<NotificacionResponseType>>>;

public class GetNotificacionByIdentificacionAppQueryHandler : IRequestHandler<GetNotificacionByIdentificacionAppQuery, ResponseType<List<NotificacionResponseType>>>
{
    private readonly IRepositoryAsync<BitacoraNotificacion> _repositoryAsync;
    private readonly IRepositoryAsync<Cliente> _repositoryClAsync;
    private readonly IRepositoryAsync<NotificacionMotivo> _repositoryNMAsync;
    private readonly IRepositoryAsync<SolicitudJustificacion> _repositoryJustiAsync;
    private readonly IRepositoryAsync<SolicitudVacacion> _repositoryVacaAsync;
    private readonly IRepositoryAsync<SolicitudPermiso> _repositoryPermiAsync;
    private readonly IConfiguration _config;
    private readonly ILogger<GetNotificacionByIdentificacionQueryHandler> _log;

    public GetNotificacionByIdentificacionAppQueryHandler(IRepositoryAsync<BitacoraNotificacion> repositoryAsync,
        IRepositoryAsync<Cliente> repositoryClAsync, IConfiguration config, ILogger<GetNotificacionByIdentificacionQueryHandler> log,
        IRepositoryAsync<NotificacionMotivo> repositoryNMAsync, IRepositoryAsync<SolicitudJustificacion> repositoryJustiAsync,
        IRepositoryAsync<SolicitudVacacion> repositoryVacaAsync,
       IRepositoryAsync<SolicitudPermiso> repositoryPermiAsync)
    {

        _repositoryAsync = repositoryAsync;
        _repositoryClAsync = repositoryClAsync;
        _repositoryNMAsync = repositoryNMAsync;
        _repositoryJustiAsync = repositoryJustiAsync;
        _config = config;
        _log = log;
        _repositoryPermiAsync = repositoryPermiAsync;
        _repositoryVacaAsync = repositoryVacaAsync;
    }

    public async Task<ResponseType<List<NotificacionResponseType>>> Handle(GetNotificacionByIdentificacionAppQuery request, CancellationToken cancellationToken)
    {
        try
        {
            //bool estadoRequerido = false;
            var diasAntiguedadNotific = _config.GetSection("Notificaciones:diasAntiguedadNotific").Get<string>();
            var uidNotificPorAprobar = Guid.Parse(_config.GetSection("Notificaciones:UidNotificPorAprobar").Get<string>());

            var fechaHasta = DateTime.Now.Date;
            var fechaDesde = fechaHasta.AddDays(-Convert.ToInt32(diasAntiguedadNotific)).Date;

            var suscriptor = await _repositoryClAsync.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(request.Identificacion), cancellationToken);
            var estadoLeido = request.EstadoLeido ? "S" : "N";

            var listaNotificaciones = await _repositoryAsync.ListAsync(new NotificacionByIdentificacionSpec(request.Identificacion, fechaDesde, fechaHasta,estadoLeido), cancellationToken);
            var listaNotificacionesByPadre = await _repositoryAsync.ListAsync(new NotificacionByIdentificacionSuscriptorPadreSpec(fechaDesde, fechaHasta, suscriptor.Id, uidNotificPorAprobar,estadoLeido), cancellationToken);
            var listAprobNotifiMotivo = await _repositoryNMAsync.ListAsync(new ListTipoMotivoSpec());
            List<BitacoraNotificacion> notificacionMotivo = new();


             var listNotificaciones = await _repositoryAsync.ListAsync(new NotificacionByIdentificacionSpec("", fechaDesde, fechaHasta, estadoLeido), cancellationToken);

            foreach (var item in listAprobNotifiMotivo)
            {
                if (item.Feature.Codigo == "JUS")
                {
                    var list = listNotificaciones.Where(x => x.EventoDifusion.Evento.Feature.Codigo == item.Feature.Codigo && x.EventoDifusion.ClasificacionId == uidNotificPorAprobar).ToList();
                    foreach (var item2 in list)
                    {
                        var objSolicitud = await _repositoryJustiAsync.FirstOrDefaultAsync(new SolicitudJustificacionByIdSpec(item2.SolicitudId), cancellationToken);
                        if (objSolicitud is not null)
                        {
                            if (objSolicitud.TipoJustificacion.Id == item.TipoMotivoId)
                            {
                                notificacionMotivo.AddRange(list.Where(x => x.SolicitudId == objSolicitud.Id && suscriptor.Id == item.AprobadorId));
                            }
                        }

                    }

                }
                else
                {
                    var list = listNotificaciones.Where(x => x.EventoDifusion.Evento.Feature.Codigo == item.Feature.Codigo && x.EventoDifusion.ClasificacionId == uidNotificPorAprobar).ToList();
                    foreach (var item2 in list)
                    {
                        
                        var objSolicitud = await _repositoryPermiAsync.FirstOrDefaultAsync(new SolicitudPermisoByIdSpec(item2.SolicitudId), cancellationToken);
                        if (objSolicitud is not null)
                        {
                            if (objSolicitud.TipoPermiso.Id == item.TipoMotivoId)
                            {
                                notificacionMotivo.AddRange(list.Where(x => x.SolicitudId == objSolicitud.Id && suscriptor.Id == item.AprobadorId));
                            }
                        }

                    }
                }

            }
            var resultNotific = (from s in listaNotificaciones
                                 where s.EventoDifusion.ClasificacionId != uidNotificPorAprobar
                                 select s).ToList();

            var resultNotificByPadre = (from s in listaNotificacionesByPadre
                                        where s.EventoDifusion.ClasificacionId == uidNotificPorAprobar
                                        select s).ToList();

            if (notificacionMotivo.Any())
            {
                resultNotific.AddRange(notificacionMotivo.OrderByDescending(x => x.FechaCreacion));
                resultNotific.AddRange(resultNotificByPadre);
            }
            else
            {
                resultNotific.AddRange(resultNotificByPadre);
            }

            var result = (from x in resultNotific //listaNotificaciones
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
            var solPermiso = result.Where(x => x.TipoSolicitud == "PER").ToList();
            var solVac = result.Where(x => x.TipoSolicitud == "VAC").ToList();
            var solJust = result.Where(x => x.TipoSolicitud == "JUST").ToList();

            var idEstadoSol = Guid.Parse(_config.GetSection("EstadosWorkflow:Solicitada").Get<string>());

            foreach (var itemJust in solJust)
            {
                var solEstado = await _repositoryJustiAsync.GetByIdAsync(itemJust.SolicitudId);
                if (solEstado != null)
                {
                    if (solEstado.IdEstadoSolicitud != idEstadoSol)
                    {
                        result.RemoveAll(x => x.SolicitudId == solEstado.Id);
                    }
                }

            }

            foreach (var itemVac in solVac)
            {
                var solEstado = await _repositoryVacaAsync.GetByIdAsync(itemVac.SolicitudId);

                if (solEstado != null)
                {
                    if (solEstado.IdEstadoSolicitud != idEstadoSol)
                    {
                        result.RemoveAll(x => x.SolicitudId == solEstado.Id && x.CodigoClasificacion == "TRAM_PROC");
                    }
                }


            }

            foreach (var itemPer in solPermiso)
            {
                var solEstado = await _repositoryPermiAsync.GetByIdAsync(itemPer.SolicitudId);

                if (solEstado != null)
                {
                    if (solEstado.IdEstadoSolicitud != idEstadoSol)
                    {
                        result.RemoveAll(x => x.SolicitudId == solEstado.Id);
                    }
                }

            }
            return new ResponseType<List<NotificacionResponseType>>() { Data = result, Succeeded = true, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000" };
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<List<NotificacionResponseType>>() { Data = null, Succeeded = true, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500" };
        }
    }

}