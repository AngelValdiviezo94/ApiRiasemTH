using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Familiares.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Familiares;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace EnrolApp.Application.Features.Clients.Commands.SuscriptorRestableceContrasena
{
    public record SuscriptorRestableceContrasenaCommand(SuscriptorRestableceContrasenaRequest SuscriptorRequest) : IRequest<ResponseType<string>>;
    public class SuscriptorRestableceContrasenaCommandHandler : IRequestHandler<SuscriptorRestableceContrasenaCommand, ResponseType<string>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsyncCl;
        private readonly IRepositoryAsync<FamiliarColaborador> _repositoryAsyncFamCol;
        private readonly IApisConsumoAsync _repositoryApis;
        private readonly IConfiguration _config;
        private readonly ILogger<SuscriptorRestableceContrasenaCommandHandler> _log;
        private readonly string UrlBaseApiAuth = string.Empty;
        private readonly string UrlBaseApiUtils = string.Empty;
        private readonly string UrlBaseApiBuenDia = string.Empty;
        private string nombreEnpoint = string.Empty;
        private string uriEnpoint = string.Empty;

        public SuscriptorRestableceContrasenaCommandHandler(IConfiguration config, IRepositoryAsync<Cliente> repositoryAsyncCl, IRepositoryAsync<FamiliarColaborador> repositoryAsyncFamCol,
            IApisConsumoAsync repositoryApis, ILogger<SuscriptorRestableceContrasenaCommandHandler> log)
        {
            _repositoryAsyncCl = repositoryAsyncCl;
            _repositoryAsyncFamCol = repositoryAsyncFamCol;
            _repositoryApis = repositoryApis;
            _config = config;
            _log = log;
            UrlBaseApiAuth = _config.GetSection("ConsumoApis:UrlBaseApiAuth").Get<string>();
            UrlBaseApiUtils = _config.GetSection("ConsumoApis:UrlBaseApiUtils").Get<string>();
            UrlBaseApiBuenDia = _config.GetSection("ConsumoApis:UrlBaseApiBuenDia").Get<string>();
        }

        public async Task<ResponseType<string>> Handle(SuscriptorRestableceContrasenaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var req = request.SuscriptorRequest;
                string tipoColaborador = string.IsNullOrEmpty(req.TipoColaborador) ? string.Empty : req.TipoColaborador.ToUpper();

                if (tipoColaborador == "F")
                {
                    var familiar = await _repositoryAsyncFamCol.FirstOrDefaultAsync(new GetFamiliarColaboradorByIdentificacionSpec(req.Identificacion), cancellationToken);

                    if (familiar is null)
                        return new ResponseType<string>() { Succeeded = false, Data = null, Message = "Identificación no registrada", StatusCode = "201" };

                    if (familiar.Estado == "I")
                        return new ResponseType<string>() { Succeeded = false, Data = null, Message = "Familiar no se encuentra suscrito", StatusCode = "201" };

                    if (!familiar.Habilitado)
                        return new ResponseType<string>() { Succeeded = false, Data = null, Message = "Familiar no se encuentra habilitado", StatusCode = "201" };

                    //var objOtp = new
                    //{
                    //    Codigo = req.Otp,
                    //    Cedula = req.Identificacion
                    //};

                    //nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ValidarOtp").Get<string>();
                    //uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
                    //var (SuccessOtp, DataOtp) = await _repositoryApis.PutEndPoint(objOtp, uriEnpoint, nombreEnpoint);

                    var objUpdateUser = new
                    {
                        identificacion = req.Identificacion,
                        password = req.Password
                    };

                    nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ActualizarUsuarioLdap").Get<string>();
                    uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
                    var (Success, Data) = await _repositoryApis.PutEndPoint(objUpdateUser, uriEnpoint, nombreEnpoint);

                    if (Success)
                    {
                        var objEnviarCorreo = new
                        {
                            otp = string.Empty,
                            celular = familiar.Celular,
                            alias = familiar.Alias,
                            para = familiar.Correo,
                            plantilla = _config.GetSection("RestableceContrasena:PlantillaNotificacionCambioContrasena").Get<string>() ?? string.Empty,
                            asunto = _config.GetSection("RestableceContrasena:AsuntoNotificacionCambioContrasena").Get<string>() ?? string.Empty
                        };

                        nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo").Get<string>();
                        uriEnpoint = UrlBaseApiUtils + nombreEnpoint;
                        var resultCorreo = await _repositoryApis.PostEndPoint(objEnviarCorreo, uriEnpoint, nombreEnpoint);
                    }
                    else
                    {
                        return new ResponseType<string>() { Succeeded = false, Data = null, Message = "No se pudo restablecer la contraseña", StatusCode = "201" };
                    }
                }
                else
                {
                    var suscriptor = await _repositoryAsyncCl.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(request.SuscriptorRequest.Identificacion), cancellationToken);

                    if (suscriptor is null)
                        return new ResponseType<string>() { Succeeded = false, Data = null, Message = "Identificación no registrada", StatusCode = "201" };

                    if (suscriptor.Estado == "I")
                        return new ResponseType<string>() { Succeeded = false, Data = null, Message = "Colaborador no se encuentra suscrito", StatusCode = "201" };

                    //var objOtp = new
                    //{
                    //    Codigo = req.Otp,
                    //    Cedula = req.Identificacion
                    //};

                    //nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ValidarOtp").Get<string>();
                    //uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
                    //var (SuccessOtp, DataOtp) = await _repositoryApis.PostEndPoint(objOtp, uriEnpoint, nombreEnpoint);

                    var objUpdateUser = new
                    {
                        identificacion = suscriptor.Identificacion,
                        password = request.SuscriptorRequest.Password
                    };

                    nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ActualizarUsuarioLdap").Get<string>();
                    uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
                    var (Success, Data) = await _repositoryApis.PutEndPoint(objUpdateUser, uriEnpoint, nombreEnpoint);

                    var objUpdateUserBuenDia = new
                    {
                        LogonName = suscriptor.Identificacion,
                        Password = request.SuscriptorRequest.Password,
                        ConfirmPassword = string.Empty
                    };

                    nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiBuenDia:RecoveryPassword").Get<string>();
                    uriEnpoint = UrlBaseApiBuenDia + nombreEnpoint;

                    using var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    var response = await client.PostAsJsonAsync(uriEnpoint, objUpdateUserBuenDia, cancellationToken: cancellationToken);
                    var buendiaResponse = JsonConvert.DeserializeObject<RecoveryPassword>(response.Content.ReadAsStringAsync(cancellationToken).Result);

                    if (Success && buendiaResponse.IsValid)
                    {
                        var objEnviarCorreo = new
                        {
                            otp = string.Empty,
                            celular = suscriptor.Celular,
                            alias = suscriptor.Alias,
                            para = suscriptor.Correo,
                            plantilla = _config.GetSection("RestableceContrasena:PlantillaNotificacionCambioContrasena").Get<string>() ?? string.Empty,
                            asunto = _config.GetSection("RestableceContrasena:AsuntoNotificacionCambioContrasena").Get<string>() ?? string.Empty
                        };

                        nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo").Get<string>();
                        uriEnpoint = UrlBaseApiUtils + nombreEnpoint;
                        var resultCorreo = await _repositoryApis.PostEndPoint(objEnviarCorreo, uriEnpoint, nombreEnpoint);
                    }
                    else
                    {
                        return new ResponseType<string>() { Succeeded = false, Data = null, Message = "No se pudo restablecer la contraseña", StatusCode = "201" };
                    }
                }

                return new ResponseType<string>() { Succeeded = true, Data = null, Message = "Contraseña restablecida satisfactoriamente", StatusCode = "200" };
            }
            catch (Exception e)
            {
                _log.LogError(e, string.Empty);
                return new ResponseType<string>() { Succeeded = false, Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500" };
            }
        }
    }

}
