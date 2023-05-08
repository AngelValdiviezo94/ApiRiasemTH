using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Banner.Commands.GetListadoBanner;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebEnrolAppApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BannerController : ApiControllerBase
    {
        [HttpGet("GetListadoBanner")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetColaboradores(string identificacion, Guid uidCanal, CancellationToken cancellationToken, string? tipoColaborador = "C")
        {
            var objResult = await Mediator.Send(new GetListadoBannerCommand(identificacion, uidCanal, tipoColaborador), cancellationToken);

            return Ok(objResult);
        }
    }
}