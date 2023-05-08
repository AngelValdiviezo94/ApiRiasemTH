using EnrolApp.Application.Features.Billeteras.Dto;
using EnrolApp.Application.Features.Billeteras.Queries.GetSaldoContableByIdentificacion;
using EnrolApp.Application.Features.Wallets.Dto;
using EnrolApp.Application.Features.Wallets.Queries.GetCupoCredito;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebEnrolAppApi.Controllers.v1;

[ApiVersion("1.0")]
public class BilleteraController : ApiControllerBase
{
    /// <summary>
    /// Información del cupo disponible del colaborador
    /// </summary>
    /// <param name="authToken">token de EnrolApp</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    [HttpGet("GetCupoCredito")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(CupoCreditoResponseType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCupoCredito([FromHeader(Name = "Authorization")] string authToken, CancellationToken cancellationToken)
    {
        var query = new GetCupoCreditoQuery(authToken);
        var objResult = await Mediator.Send(query, cancellationToken);
        return Ok(objResult);
    }
    /// <summary>
    /// Información del Saldo Contable disponible por el colaborador
    /// </summary>
    /// <param name="Identificacion">identificacion del empleado</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    [HttpGet("GetSaldoContableByIdentificacion/{Identificacion}")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(SaldoContableResponseType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaldoContableByIdentificacion(string Identificacion, CancellationToken cancellationToken)
    {
        var query = new GetSaldoContableByIdentificacionQuery(Identificacion);
        var objResult = await Mediator.Send(query, cancellationToken);
        return Ok(objResult);
    }
}
