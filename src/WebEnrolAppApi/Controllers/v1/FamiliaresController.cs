using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Familiares.Commands.CreateFamiliarColaborador;
using EnrolApp.Application.Features.Familiares.Commands.GetInfoFamiliarColaborador;
using EnrolApp.Application.Features.Familiares.Commands.GetListadoFamiliarColaborador;
using EnrolApp.Application.Features.Familiares.Commands.GetTipoRelacionFamiliar;
using EnrolApp.Application.Features.Familiares.Commands.UpdateFamiliarColaborador;
using EnrolApp.Application.Features.Familiares.Commands.UpdateSesionFamilar;
using EnrolApp.Application.Features.Familiares.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebEnrolAppApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FamiliaresController : ApiControllerBase
    {
        /// <summary>
        /// Consulta la información de los tipos de relación familiar
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetTipoRelacionFamiliar")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<List<ResponseTipoRelacionFamiliarType>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTipoRelacionFamiliar(CancellationToken cancellationToken)
        {
            var objResult = await Mediator.Send(new GetTipoRelacionFamiliarCommand(), cancellationToken);

            return Ok(objResult);
        }

        /// <summary>
        /// Crear familiar para un colaborador registrado
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("CreateFamiliarColaborador")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateFamiliarColaborador(CreateFamiliarColaboradorRequest request, CancellationToken cancellationToken)
        {
            var authToken = HttpContext.Request.Headers["Authorization"].ToString();
            var identificacion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;
            request.IdentificacionColaborador = identificacion;

            var objResult = await Mediator.Send(new CreateFamiliarColaboradorCommand(request, authToken), cancellationToken);

            return Ok(objResult);
        }

        [HttpGet("GetListadoFamiliarColaborador")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<ResponseFamiliarColaboradorType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListadoFamiliarColaborador(string colaboradorId, CancellationToken cancellationToken)
        {
            var objResult = await Mediator.Send(new GetListadoFamiliarColaboradorCommand(colaboradorId), cancellationToken);

            return Ok(objResult);
        }
        

        [HttpGet("GetInfoFamiliarColaborador")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<ResponseFamiliarColaboradorType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInfoFamiliarColaborador(string identificacionFamiliar, CancellationToken cancellationToken)
        {
            if (identificacionFamiliar is null)
            {
                throw new ArgumentNullException(nameof(identificacionFamiliar));
            }

            var objResult = await Mediator.Send(new GetInfoFamiliarColaboradorCommand(identificacionFamiliar), cancellationToken);

            return Ok(objResult);
        }

        [HttpPut("UpdateFamiliarColaborador")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateFamiliarColaborador(UpdateFamiliarColaboradorRequest request, CancellationToken cancellationToken)
        {
            var authToken = HttpContext.Request.Headers["Authorization"].ToString();
            var identificacion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;
            request.IdentificacionColaborador = identificacion;

            var objResult = await Mediator.Send(new UpdateFamiliarColaboradorCommand(request, authToken), cancellationToken);

            return Ok(objResult);
        }


        /// <summary>
        /// Actualiza el token de sesion de los familiares del colaborador
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("UpdateSesionFamiliar")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateSesionFamiliar(UpdateSesionFamiliarRequest request, CancellationToken cancellationToken)
        {
            var objResult = await Mediator.Send(new UpdateSesionFamiliarCommand(request), cancellationToken);
            return Ok(objResult);
        }

    }
}
