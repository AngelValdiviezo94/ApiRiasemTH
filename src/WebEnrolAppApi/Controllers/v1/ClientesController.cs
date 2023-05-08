using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Commands.ActivateCliente;
using EnrolApp.Application.Features.Clients.Commands.CreateCliente;
using EnrolApp.Application.Features.Clients.Commands.GetListadoColaboradores;
using EnrolApp.Application.Features.Clients.Commands.InfoSuscriptorRestableceContrasena;
using EnrolApp.Application.Features.Clients.Commands.ReenviarActivacion;
using EnrolApp.Application.Features.Clients.Commands.SuscriptorRestableceContrasena;
using EnrolApp.Application.Features.Clients.Commands.UpdateContrasenaColaborador;
using EnrolApp.Application.Features.Clients.Commands.UpdateInfoColaborador;
using EnrolApp.Application.Features.Clients.Commands.UpdateInfoPersonalColaborador;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Application.Features.Clients.Queries.GetClienteById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebEnrolAppApi.Controllers.v1;

[ApiVersion("1.0")]
public class ClientesController : ApiControllerBase
{

    /// <summary>
    /// Obtener cliente por el codigo
    /// </summary>
    /// <param name="Codigo">codigo del cliente</param>
    /// <param name="Identificacion">campo opcional por identificacion</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    [HttpGet("{Codigo:int}")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ClienteType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClienteById(string? Codigo, string? Identificacion,  CancellationToken cancellationToken)
    {
        var query = new GetProspectoByIdQuery(Codigo,Identificacion);
        var cliente = await Mediator.Send(query, cancellationToken);
        return Ok(cliente);
    }

    [HttpGet("GetClienteByIdentificacion/{Identificacion}")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ClienteType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<IActionResult> GetClienteByIdentificacion(string Identificacion, CancellationToken cancellationToken)
    {
        var query = new GetProspectoByIdQuery(string.Empty, Identificacion);
        var cliente = await Mediator.Send(query, cancellationToken);
        return Ok(cliente);
    }

    /// <summary>
    /// Crea un nuevo cliente
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Retorna el codigo  uid del nuevo cliente </returns>
    /// <remarks>
    /// Ejemplo request:
    ///
    ///     POST /CreateCliente
    ///     {
    ///         "tipoidentificacion": "C",
    ///         "identificacion": "0920693975",
    ///         "genero": "M",
    ///         "latitud": "-2.093042",
    ///         "longitud": "-79.950629",
    ///         "direccion": "Cdla Los Almendros Mz 14 villa: 15",
    ///         "fechaNacimiento": "2022-07-28",
    ///         "correo": "dsanch152000@hotmail.com",
    ///         "password": "Pa$$w0rd214"
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Cliente creado</response>
    /// <response code="400">Si el registro es nulo</response>
    [HttpPost("CreateCliente")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateCliente([FromBody] CreateClienteRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new CreateClienteCommand(request), cancellationToken);
        return Ok(objResult);

    }

    /// <summary>
    /// Actualiza el estado de suscripción del prospecto a cliente
    /// </summary>
    /// <param name="identificacion"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("ActivaClienteServicio/{identificacion}")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateClienteStatus(string identificacion, CancellationToken cancellationToken)
    {

        var objResult = await Mediator.Send(new ActivateClienteCommand(identificacion), cancellationToken);
        if (objResult.Succeeded)
            return Redirect("https://enrolapp.app.link/NdJ6nFzRbK?bnc_validate=true");

        //return Ok(objResult);
        return Redirect("https://enrolapp.app.link/NdJ6nFzRbK?bnc_validate=false");
    }
    

    /// <summary>
    /// Retorna información visualización restablecer contraseña
    /// </summary>
    /// <param name="identificacion"></param>
    /// <param name="enviaOtp"></param>
    /// <param name="tipoColaborador"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("InfoSuscriptorRestableceContrasena/{identificacion}/{enviaOtp}")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<SuscriptorRestableceContrasenaType>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> InfoSuscriptorRestableceContrasena(string identificacion, bool enviaOtp, CancellationToken cancellationToken, string? tipoColaborador = "C")
    {
        var objResult = await Mediator.Send(new InfoSuscriptorRestableceContrasenaCommand(identificacion, enviaOtp, tipoColaborador), cancellationToken);
        return Ok(objResult);
    }
    /// <summary>
    /// Reestablecer la contraseña
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("SuscriptorRestableceContrasena")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> SuscriptorRestableceContrasena([FromBody] SuscriptorRestableceContrasenaRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new SuscriptorRestableceContrasenaCommand(request), cancellationToken);
        return Ok(objResult);
    }


    /// <summary>
    /// Reenviar el correo de activación en caso de no enviarse
    /// </summary>
    /// <param name="Identificacion"></param>
    /// <param name="Correo"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("ReenviarActivacion")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> ReenviarActivacion(string Identificacion, string? Correo, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new ReenviarActivacionCommand(Identificacion, Correo), cancellationToken);
        return Ok(objResult);
    }

    [HttpGet("GetListadoColaboradores")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetColaboradores(string? codUdn, string? codArea, string? codScc, string? colaborador, CancellationToken cancellationToken)
    {
        GetListadoColaboradoresRequest request = new GetListadoColaboradoresRequest
        {
            Udn = codUdn ?? "",
            Area = codArea ?? "",
            Scc = codScc ?? "",
            Colaborador = colaborador ?? ""
        };

        var objResult = await Mediator.Send(new GetListadoColaboradoresCommand(request), cancellationToken);

        return Ok(objResult);
    }

    [HttpPut("UpdateInfoColaborador")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateInfoColaborador(UpdateInfoColaboradorRequest request, CancellationToken cancellationToken)
    {
        var Identificacion = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == "Identificacion")?.Value ?? string.Empty;
        request.IdentificacionModifica = Identificacion;

        var objResult = await Mediator.Send(new UpdateInfoColaboradorCommand(request), cancellationToken);
        return Ok(objResult);
    }

    [HttpPut("UpdateInfoPersonalColaborador")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateInfoPersonalColaborador(ListadoColaboradoresType request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new UpdateInfoPersonalColaboradorCommand(request), cancellationToken);
        return Ok(objResult);
    }

    [HttpPut("UpdateContrasenaColaborador")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateContrasenaColaborador(UpdateContrasenaColaboradorRequest request, CancellationToken cancellationToken)
    {
        var objResult = await Mediator.Send(new UpdateContrasenaColaboradorCommand(request), cancellationToken);
        return Ok(objResult);
    }

}