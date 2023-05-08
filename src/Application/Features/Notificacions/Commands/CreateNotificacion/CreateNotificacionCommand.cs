
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Notifications.Specifications;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Notificacion;
using MediatR;
using EnrolApp.Application.Common.Exceptions;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Notifications.Commands.CreateNotificacion;

public record CreateNotificacionCommand(CreateNotificacionRequest ReqNotif) : IRequest<ResponseType<string>>;

public class CreateNotificacionCommandHandler : IRequestHandler<CreateNotificacionCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<BitacoraNotificacion> _repoNotifAsync;
    private readonly IRepositoryAsync<EventoDifusion> _repoEvDifAsync;
    private readonly IRepositoryAsync<Cliente> _repoCliAsync;
    private readonly ILogger<CreateNotificacionCommandHandler> _log;

    public CreateNotificacionCommandHandler(IRepositoryAsync<BitacoraNotificacion> repoNotifAsync,
        IRepositoryAsync<EventoDifusion> repoEvDifAsync, IRepositoryAsync<Cliente> repoCliAsync, ILogger<CreateNotificacionCommandHandler> log)
    {
        _log = log;
        _repoNotifAsync = repoNotifAsync;
        _repoEvDifAsync = repoEvDifAsync;
        _repoCliAsync = repoCliAsync;
    }

    public async Task<ResponseType<string>> Handle(CreateNotificacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Guid? SolicitudId = null;
            string TipoSolicitud = "";
            string IdTransaccion = "";
            string IdPuntoOperacion = "";
            var listaEveDifusion = await _repoEvDifAsync.ListAsync(new EventoDifusionByCodigoSpec(request.ReqNotif.CodigoEvento), cancellationToken);
            
            if (listaEveDifusion.Any())
            {
                List<BitacoraNotificacion> lstBitacora = new();
                var objCliente = await _repoCliAsync.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(request.ReqNotif.Identificacion), cancellationToken);
                if (objCliente == null)
                {
                    return new ResponseType<string>() { Succeeded = false, Message = CodeMessageResponse.GetMessageByCode("102"), StatusCode = "102" };
                }

                foreach (var item in listaEveDifusion)
                {
                    string mensaje = item.Plantilla.Mensaje;
                    if (item.Plantilla.RequiereEvalVariables)
                    {
                        if (request.ReqNotif.ListaParamValue.ContainsKey("nombreEmpleado"))
                        {
                            var nombreEmpleado = request.ReqNotif.ListaParamValue["nombreEmpleado"].ToString();
                            mensaje = mensaje.Replace("@empleado", nombreEmpleado);
                            item.Plantilla.Mensaje = mensaje;
                        }

                        mensaje = ProcesarContenidoMensaje(item.Plantilla, request.ReqNotif.ListaParamValue);

                    }

                    if (request.ReqNotif.ListaParamValue.ContainsKey("solicitudId"))
                    {
                        SolicitudId = Guid.Parse(request.ReqNotif.ListaParamValue["solicitudId"]);
                    }

                    if (request.ReqNotif.ListaParamValue.ContainsKey("tipoSolicitud"))
                    {
                        TipoSolicitud = request.ReqNotif.ListaParamValue["tipoSolicitud"].ToString();
                    }

                    if (request.ReqNotif.ListaParamValue.ContainsKey("idTransaccion"))
                    {
                        IdTransaccion = request.ReqNotif.ListaParamValue["idTransaccion"].ToString();
                    }

                    if (request.ReqNotif.ListaParamValue.ContainsKey("idPuntoOperacion"))
                    {
                        IdPuntoOperacion = request.ReqNotif.ListaParamValue["idPuntoOperacion"].ToString();
                    }

                    var obj = new BitacoraNotificacion()
                    {
                        ClienteId = objCliente.Id,
                        EventoDifusionId = item.Id,
                        SolicitudId = SolicitudId,
                        TipoSolicitud = TipoSolicitud,
                        Identificacion = request.ReqNotif.Identificacion,
                        Resumen = item.Plantilla.Resumen,
                        Mensaje = mensaje,
                        MensajeHtml = item.Plantilla.MensajeHtml,
                        FechaCreacion = DateTime.Now,
                        IdTransaccion = IdTransaccion,
                        IdPuntoOperacion = IdPuntoOperacion
                    };
                    lstBitacora.Add(obj);
                }
                if (lstBitacora.Any())
                {
                    await _repoNotifAsync.AddRangeAsync(lstBitacora, cancellationToken);
                    return new ResponseType<string>() { Data = null, Message = "Notificaciones registradas exitosamente", StatusCode = "100", Succeeded = true };
                }
            }
            else
            {
                return new ResponseType<string>() { Data = null, Message = "No existe configuración de notificaciones para codigo evento ingresado", StatusCode = "102", Succeeded = false };
            }

            return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("102"), StatusCode = "102", Succeeded = false };
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
        }
    }


    private static string ProcesarContenidoMensaje(Plantilla Obj_plan, Dictionary<string, string> ListaParamVal)
    {
        if (ListaParamVal is not null)
        {
            string mensaje = Obj_plan.Mensaje;

            switch (Obj_plan.Codigo)
            {
                case "PLA_INFO_ROL":
                    {
                        var anio = Convert.ToInt32(ListaParamVal["FechaCorte"].ToString().Split('-')[0]);
                        var mes = Convert.ToInt32(ListaParamVal["FechaCorte"].ToString().Split('-')[1]);
                        DateTime fechaCorte = new(anio, mes, 1);
                        string mesFormateado = fechaCorte.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
                        mesFormateado = char.ToUpper(mesFormateado[0]) + mesFormateado.Substring(1);
                        mensaje = mensaje.Replace("@mes", mesFormateado);
                    }
                    break;
                case "PLAN_INFO_GEN_PEDI_ROL":
                    {
                        var nombreEmpleado = ListaParamVal["nombreEmpleado"].ToString();
                        var totalCompra = ListaParamVal["totalCompra"].ToString();
                        mensaje = mensaje.Replace("@empleado", nombreEmpleado);
                        mensaje = mensaje.Replace("@total", totalCompra);
                    }
                    break;
                default:
                    break;
            }

            return mensaje;
        }
        else
        {
            return Obj_plan.Mensaje;
        }
    }






}
