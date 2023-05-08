using EnrolApp.Application.Features.Employees.Dto;
using EnrolApp.Application.Features.Employees.Queries.GetInfoGeneralByIdentificacion;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebEnrolAppApi.Controllers.v1;

[ApiVersion("1.0")]
public class EmpleadosController : ApiControllerBase
{

    /// <summary>
    /// Obtiene informacion general del empleado
    /// </summary>
    /// <param name="Identificacion">identificacion del empleado</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{Identificacion}")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(InformacionGeneralEmpleadoType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetInfoGeneralByIdentificacion(string Identificacion, CancellationToken cancellationToken)
    {
        var query = new GetInfoGeneralByIdentificacionQuery(Identificacion);
        var cliente = await Mediator.Send(query, cancellationToken);
        return Ok(cliente);
    }

}
