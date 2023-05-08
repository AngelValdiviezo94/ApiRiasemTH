using EnrolApp.Application.Features.Employees.Dto;
using EnrolApp.Application.Features.Employees.Queries.GetAvisoEntradaByIdentificacion;
using EnrolApp.Application.Features.Employees.Queries.GetCertifLaboralByIdentificacion;
using EnrolApp.Application.Features.Employees.Queries.GetRolPagoByFilter;
using EnrolApp.Application.Features.Notifications.Commands.CreateNotificacion;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebEnrolAppApi.Controllers.v1;

[ApiVersion("1.0")]
public class ReportesController : ApiControllerBase
{

    /// <summary>
    /// Informaciòn del colaborador para generar un certificado laboral
    /// </summary>
    /// <param name="Identificacion">identificacion del empleado</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    [HttpGet("GetCertifLaboralByIdentificacion/{Identificacion}")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(CertificadoLaboralType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCertifLaboralByIdentificacion(string Identificacion, CancellationToken cancellationToken)
    {
        var query = new GetCertifLaboralByIdentificacionQuery(Identificacion);
        var objResult = await Mediator.Send(query, cancellationToken);
        if (objResult is not null)
        {
            CreateNotificacionRequest objReqNotif = new()
            {
                Identificacion = Identificacion,
                CodigoEvento = "GEN_CERT_LAB"
            };
            var objNotif = await Mediator.Send(new CreateNotificacionCommand(objReqNotif), cancellationToken);
            if (!objNotif.Succeeded) return Ok(objNotif);
        }
        return Ok(objResult);
    }


    /// <summary>
    /// Informaciòn del colaborador para generar un aviso de entrada
    /// </summary>
    /// <param name="Identificacion">identificacion del empleado</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    [HttpGet("GetAvisoEntradaByIdentificacion/{Identificacion}")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(AvisoEntradaType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAvisoEntradaByIdentificacion(string Identificacion, CancellationToken cancellationToken)
    {
        var query = new GetAvisoEntradaByIdentificacionQuery(Identificacion);
        var objResult = await Mediator.Send(query, cancellationToken);
        if (objResult is not null)
        {
            CreateNotificacionRequest objReqNotif = new()
            {
                Identificacion = Identificacion,
                CodigoEvento = "GEN_REG_INGR"
            };
            var objNotif = await Mediator.Send(new CreateNotificacionCommand(objReqNotif), cancellationToken);
            if (!objNotif.Succeeded) return Ok(objNotif);
        }
        return Ok(objResult);
    }


    [HttpGet("GetRolPago/{Identificacion}/{FechaCorte}")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(RolPagoType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRolPago(string Identificacion,string FechaCorte, CancellationToken cancellationToken)
    {
        var query = new GetRolPagoByFilterQuery(Identificacion, FechaCorte);
        var objResult = await Mediator.Send(query, cancellationToken);
        if (objResult is not null)
        {
            CreateNotificacionRequest objReqNotif = new()
            {
                Identificacion = Identificacion,
                CodigoEvento = "GEN_ROLPAGO",
                ListaParamValue = new(){{ "FechaCorte", FechaCorte }}
            };

       
            var objNotif =  await Mediator.Send(new CreateNotificacionCommand(objReqNotif), cancellationToken);
            if(!objNotif.Succeeded) return Ok(objNotif);
        }
        return Ok(objResult);
    }

}
