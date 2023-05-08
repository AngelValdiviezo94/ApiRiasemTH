using Microsoft.AspNetCore.Mvc;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Horarios.Commands.GetHorario;
using EnrolApp.Application.Features.Horarios.Dto;
using EnrolApp.Application.Features.Horarios.Queries.GetHorarioByFilter;
using EnrolApp.Application.Features.Horarios.Commands.CreateMarcacion;
using System.IdentityModel.Tokens.Jwt;

namespace WebEnrolAppApi.Controllers.v1;

[ApiVersion("1.0")]
public class HorariosController : ApiControllerBase
{
    /// <summary>
    /// Consulta Horario Laboral en base a un rango de fechas
    /// </summary>
    /// <param name="identificacion"></param>
    /// <param name="fechaDesde"></param>
    /// <param name="fechaHasta"></param>
    /// <param name="cancellationToken"></param>
    ///     GET /ConsultaHorario
    ///    "identificacion": "0920693975",
    ///    "fechaDesde": "2022-07-28",
    ///    "fechaHasta": "2022-07-30"
    /// <returns></returns>
    [HttpGet("GetHorario")]
    [ProducesResponseType(typeof(ResponseType<HorarioType>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetHorario(string identificacion, string fechaDesde, string fechaHasta, CancellationToken cancellationToken)
    {
        var token = this.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1] ?? string.Empty;

        var query = new GetHorarioByFilterQuery(identificacion, fechaDesde, fechaHasta, token);
        var objResult = await Mediator.Send(query, cancellationToken);
        return Ok(objResult);
    }

    /*
    /// <summary>
    /// Registro de marcación de entrada y salida
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("GenerarMarcacion")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMarcacion([FromBody] CreateMarcacionRequest request, CancellationToken cancellationToken)
    {
        var query = new CreateMarcacionCommand(request);
        var objResult = await Mediator.Send(query, cancellationToken);
        return Ok(objResult);
    }
    */
}
