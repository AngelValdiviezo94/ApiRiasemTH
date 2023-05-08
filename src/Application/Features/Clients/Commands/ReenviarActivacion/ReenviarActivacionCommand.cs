using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Prospectos.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Suscripcion;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Clients.Commands.ReenviarActivacion;

public record ReenviarActivacionCommand(string Identificacion, string Correo) : IRequest<ResponseType<string>>;

public class ReenviarActivacionCommandHandler : IRequestHandler<ReenviarActivacionCommand, ResponseType<string>>
{
    private readonly IConfiguration _config;
    private readonly IRepositoryAsync<Cliente> _repoClieAsync;
    private readonly IRepositoryAsync<Prospecto> _repoProspAsync;
    private readonly ILogger<ReenviarActivacionCommandHandler> _log;
    private readonly IApisConsumoAsync _repositoryApis;
    private readonly string UrlBaseApiUtils = "";
    private readonly string UrlBaseApiAuth = "";
    private readonly string Esquema = string.Empty;
    private string nombreEnpoint = "";
    private string uriEnpoint = "";


    public ReenviarActivacionCommandHandler(IRepositoryAsync<Cliente> repository, IRepositoryAsync<Prospecto> repoProspAsync,
        IApisConsumoAsync repositoryApis,
    IConfiguration config, ILogger<ReenviarActivacionCommandHandler> log)
    {
        _repoClieAsync = repository;
        _repoProspAsync = repoProspAsync;
        _repositoryApis = repositoryApis;
        _config = config;
        _log = log;
        UrlBaseApiUtils = _config.GetSection("ConsumoApis:UrlBaseApiUtils").Get<string>();
        UrlBaseApiAuth = _config.GetSection("ConsumoApis:UrlBaseApiAuth").Get<string>();
        //UrlBaseApiEcommerce = _config.GetSection("ConsumoApis:UrlBaseApiEcommerce").Get<string>();
        //UrlBaseApiBuenDia = _config.GetSection("ConsumoApis:UrlBaseApiBuenDia").Get<string>();
        //ConnectionString = _config.GetConnectionString("Bd_Rrhh_Panacea");
        Esquema = _config.GetSection("StoredProcedure:Esquema").Get<string>();
    }


    public async Task<ResponseType<string>> Handle(ReenviarActivacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool updEmail = false;
            var objCliente = await _repoClieAsync.FirstOrDefaultAsync(new ClienteByIdentificacionSpec("C", request.Identificacion), cancellationToken);

            // se actualiza tiempo límite para la activación
            nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ActualizarUsuarioLdap").Get<string>();
            var uriActivateLdapUser = UrlBaseApiAuth + nombreEnpoint;
            
            await _repositoryApis.PostEndPoint(new { identificacion = request.Identificacion }, uriActivateLdapUser, nombreEnpoint);

            if (!string.IsNullOrEmpty(request.Correo) && request.Correo != objCliente.Correo)
            {
                objCliente.Correo = request.Correo;
                updEmail = true;
            }

            var objEnviar = new
            {
                para = objCliente.Correo,
                alias = objCliente?.Alias,
                identificacion = objCliente.Identificacion,
                plantilla = _config.GetSection("Notificaciones:PlantillaActivaServicio").Get<string>() ?? string.Empty,
                asunto = _config.GetSection("Notificaciones:AsuntoActivaServicio").Get<string>() ?? string.Empty
            };

            nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo").Get<string>();
            uriEnpoint = UrlBaseApiUtils + nombreEnpoint;
            await _repositoryApis.PostEndPoint(objEnviar, uriEnpoint, nombreEnpoint);

            if (updEmail)
                await _repoClieAsync.UpdateAsync(objCliente, cancellationToken);

            return new ResponseType<string>() { Data = null, Message = "Correo de Activación enviado correctamente", StatusCode = "000", Succeeded = true };
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
        }
    }

}