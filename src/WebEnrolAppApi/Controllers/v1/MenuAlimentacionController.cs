using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.MenuAlimentacion.Commands.GetMenuSemana;
using EnrolApp.Application.Features.MenuAlimentacion.Dto;
using EnrolApp.Domain.Entities.MenuSemana;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebEnrolAppApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MenuAlimentacionController : ApiControllerBase
    {
        [HttpGet("GetMenuSemana")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<MenuSemanal>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<IActionResult> GetNotificaciones(string Identificacion, string OrgaCodigo, CancellationToken cancellationToken)
        {
            GetMenuSemanaRequest objReqMenu = new()
            {
                Identificacion = Identificacion,
                OrgaCodigo = OrgaCodigo
            };
            var token = new JwtSecurityToken(this.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "TokenEcommerce")?.Value ?? string.Empty;

            var objResult = await Mediator.Send(new GetMenuSemanaCommand(objReqMenu, token), cancellationToken);
            return Ok(objResult);
        }
    }
}
