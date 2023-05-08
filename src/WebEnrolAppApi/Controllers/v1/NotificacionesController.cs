using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Notifications.Commands.CreateNotificacion;
using EnrolApp.Application.Features.Notifications.Commands.UpdateNotificacion;
using EnrolApp.Application.Features.Notifications.Dto;
using EnrolApp.Application.Features.Notifications.Queries.GetProspectoByIdentificacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebEnrolAppApi.Controllers.v1;

[ApiVersion("1.0")]
public class NotificacionesController : ApiControllerBase
{

    /// <summary>
    /// Se obtienen las notificaciones del cliente desde hace 30 dias atras por defecto
    /// </summary>
    /// <param name="Identificacion">identificacion del cliente</param>
    /// <param name="cancellationToken"></param>
    /// <param name="tipoColaborador"></param>
    /// <returns>retorna listado de notificaciones del cliente que genera EnrolApp</returns>
    [HttpGet("GetNotificaciones/{Identificacion}")]
    [ProducesResponseType(typeof(ResponseType<List<NotificacionResponseType>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNotificaciones(string Identificacion, CancellationToken cancellationToken, string? tipoColaborador = "C")
    {
        //var query = new GetNotificacionByIdentificacionQuery(Identificacion, tipoColaborador);
        //var objResult = await Mediator.Send(query, cancellationToken);
        //return Ok(objResult);
        return Ok(new ResponseType<List<NotificacionResponseType>>() { Data = null, Succeeded = true, Message = "Consulta generada exitosamente", StatusCode = "000" });
    }


    [HttpGet("GetNotificacionesApp/{Identificacion}")]
    [ProducesResponseType(typeof(ResponseType<List<NotificacionResponseType>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNotificacionesApp(string Identificacion, bool EstadoLeido, CancellationToken cancellationToken)
    {
        var query = new GetNotificacionByIdentificacionAppQuery(Identificacion,EstadoLeido);
        var objResult = await Mediator.Send(query, cancellationToken);
        return Ok(objResult);
    }



    /// <summary>
    /// Se actualiza estado de las notificaciones No leida a Leida
    /// </summary>
    /// <param name="listaIdNotif">listado de ids notificaciones</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("UpdateNotificacion")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateNotificacion([FromBody] string[] listaIdNotif, CancellationToken cancellationToken)
    {
        var query = new UpdateNotificacionCommand(listaIdNotif);
        var objResult = await Mediator.Send(query, cancellationToken);
        return Ok(objResult);
    }

    /// <summary>
    /// Ingresa una nueva notificacion
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> </returns>
    /// <remarks>
    /// Ejemplo request:
    ///
    ///     POST /CreateNotificacion
    ///     {
    ///         "identificacion": "0920693975",
    ///         "codigoEvento": "GEN_ROLPAGO"
    ///     }
    /// </remarks>
    /// <response code="201">Notificacion creada</response>
    /// <response code="400">Si el registro es nulo</response>
    [HttpPost("CreateNotificacion")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateNotificacion([FromBody] CreateNotificacionRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new CreateNotificacionCommand(request), cancellationToken);
        return Ok(objResult);

    }

}
