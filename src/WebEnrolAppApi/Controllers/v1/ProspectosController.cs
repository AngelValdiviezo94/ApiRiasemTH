using EnrolApp.Application.Features.Prospectos.Dto;
using EnrolApp.Application.Features.Prospectos.Queries.GetProspectoByIdentificacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebEnrolAppApi.Controllers.v1;

[ApiVersion("1.0")]
public class ProspectosController : ApiControllerBase
{

    /// <summary>
    /// Obtener informaciòn del prospecto por identificacion
    /// </summary>
    /// <param name="tipoProspecto">tipo prospecto</param>
    /// <param name="tipoIdentificacion">tipo identificacion</param>
    /// <param name="identificacion">Identificacion</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{tipoProspecto}/{tipoIdentificacion}/{identificacion}")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ProspectoType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<IActionResult> GetProspectoByIdentificacion(string tipoProspecto,string tipoIdentificacion,string identificacion, CancellationToken cancellationToken)
    {
        //tipoProspecto = "C";
        var query = new GetProspectoByIdentificacionQuery(tipoProspecto, tipoIdentificacion, identificacion);
        var prospecto = await Mediator.Send(query, cancellationToken);
        return Ok(prospecto); 
    }

}
