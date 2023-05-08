using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Commands.UpdateContrasenaColaborador;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Application.Features.Clients.Interfaces;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Familiares.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Familiares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace EnrolApp.Persistence.Repository.Colaborador
{
    public class ColaboradorService : IColaborador
    {
        private readonly IApisConsumoAsync _repositoryApis;
        private readonly IRepositoryAsync<Cliente> _repositoryAsyncCl;
        private readonly IRepositoryAsync<FamiliarColaborador> _repositoryAsyncFamCol;
        private readonly IConfiguration _config;
        private readonly ILogger<ColaboradorService> _log;
        private string nombreEnpoint = string.Empty;
        private readonly string urlBaseApiAuth = string.Empty;
        private readonly string urlBaseApiBuenDia = string.Empty;
        private readonly string urlBaseApiUtils = string.Empty;
        private string uriEnpoint = string.Empty;

        public ColaboradorService(IConfiguration config, ILogger<ColaboradorService> log, IApisConsumoAsync repositoryApis, 
                                    IRepositoryAsync<Cliente> repositoryAsyncCl, IRepositoryAsync<FamiliarColaborador> repositoryAsyncFamCol)
        {
            _config = config;
            _log = log;
            _repositoryApis = repositoryApis;
            _repositoryAsyncCl = repositoryAsyncCl;
            _repositoryAsyncFamCol = repositoryAsyncFamCol;
            urlBaseApiAuth = _config.GetSection("ConsumoApis:UrlBaseApiAuth").Get<string>();
            urlBaseApiUtils = _config.GetSection("ConsumoApis:UrlBaseApiUtils").Get<string>();
            urlBaseApiBuenDia = _config.GetSection("ConsumoApis:UrlBaseApiBuenDia").Get<string>();
        }

        public async Task<ResponseType<string>> UpdateContrasenaColaboradorAsync(UpdateContrasenaColaboradorRequest request)
        {
            try
            {
                var objAuntenticarUserLdap = new
                {
                    identificacion = request.Identificacion,
                    password = request.ContrasenaAnterior,
                };

                nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:AutenticarLdapUser").Get<string>();
                uriEnpoint = urlBaseApiAuth + nombreEnpoint;
                var (Success, Data) = await _repositoryApis.PostEndPoint(objAuntenticarUserLdap, uriEnpoint, nombreEnpoint);

                if (Data != null)
                {
                    var responseAutenticar = (ResponseType<string>)Data;

                    if (responseAutenticar.Succeeded && responseAutenticar.StatusCode == "100")
                    {
                        string tipoColaborador = string.IsNullOrEmpty(request.TipoColaborador) ? string.Empty : request.TipoColaborador.ToUpper();

                        if (tipoColaborador == "F")
                        {
                            var familiar = await _repositoryAsyncFamCol.FirstOrDefaultAsync(new GetFamiliarColaboradorByIdentificacionSpec(request.Identificacion));

                            var objActualizarUserLdap = new
                            {
                                identificacion = request.Identificacion,
                                password = request.ContrasenaNueva,
                            };

                            nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ActualizarUsuarioLdap").Get<string>();
                            uriEnpoint = urlBaseApiAuth + nombreEnpoint;
                            var (Success1, Data1) = await _repositoryApis.PutEndPoint(objActualizarUserLdap, uriEnpoint, nombreEnpoint);

                            if (Data1 != null)
                            {
                                var responseActualizar = (ResponseType<string>)Data1;

                                if (responseActualizar.Succeeded)
                                {
                                    var objEnviarCorreo = new
                                    {
                                        otp = string.Empty,
                                        celular = familiar.Celular,
                                        alias = familiar.Alias,
                                        para = familiar.Correo,
                                        plantilla = _config.GetSection("RestableceContrasena:PlantillaNotificacionActualizacionContrasena").Get<string>() ?? string.Empty,
                                        asunto = _config.GetSection("RestableceContrasena:AsuntoNotificacionActualizacionContrasena").Get<string>() ?? string.Empty
                                    };

                                    nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo").Get<string>();
                                    uriEnpoint = urlBaseApiUtils + nombreEnpoint;
                                    var resultCorreo = await _repositoryApis.PostEndPoint(objEnviarCorreo, uriEnpoint, nombreEnpoint);

                                    return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("200"), StatusCode = "200", Succeeded = true };
                                }
                                else
                                    return new ResponseType<string>() { Data = null, Message = "No se pudo actualizar la contraseña", StatusCode = "201", Succeeded = false };
                            }
                            else
                            {
                                return new ResponseType<string>() { Data = null, Message = "No se pudo actualizar la contraseña", StatusCode = "201", Succeeded = false };
                            }
                        }
                        else
                        {
                            var colaborador = await _repositoryAsyncCl.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(request.Identificacion));

                            var objUpdateUserBuenDia = new
                            {
                                LogonName = request.Identificacion,
                                Password = request.ContrasenaNueva,
                                ConfirmPassword = string.Empty
                            };

                            nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiBuenDia:RecoveryPassword").Get<string>();
                            uriEnpoint = urlBaseApiBuenDia + nombreEnpoint;

                            using var client = new HttpClient();
                            client.DefaultRequestHeaders.Accept.Clear();
                            var response = await client.PostAsJsonAsync(uriEnpoint, objUpdateUserBuenDia);
                            var buendiaResponse = JsonConvert.DeserializeObject<RecoveryPassword>(response.Content.ReadAsStringAsync().Result);

                            if (Success && buendiaResponse.IsValid)
                            {
                                var objActualizarUserLdap = new
                                {
                                    identificacion = request.Identificacion,
                                    password = request.ContrasenaNueva,
                                };

                                nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ActualizarUsuarioLdap").Get<string>();
                                uriEnpoint = urlBaseApiAuth + nombreEnpoint;
                                var (Success1, Data1) = await _repositoryApis.PutEndPoint(objActualizarUserLdap, uriEnpoint, nombreEnpoint);

                                if (Data1 != null)
                                {
                                    var responseActualizar = (ResponseType<string>)Data1;

                                    if (responseActualizar.Succeeded)
                                    {
                                        var objEnviarCorreo = new
                                        {
                                            otp = string.Empty,
                                            celular = colaborador.Celular,
                                            alias = colaborador.Alias,
                                            para = colaborador.Correo,
                                            plantilla = _config.GetSection("RestableceContrasena:PlantillaNotificacionActualizacionContrasena").Get<string>() ?? string.Empty,
                                            asunto = _config.GetSection("RestableceContrasena:AsuntoNotificacionActualizacionContrasena").Get<string>() ?? string.Empty
                                        };

                                        nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo").Get<string>();
                                        uriEnpoint = urlBaseApiUtils + nombreEnpoint;
                                        var resultCorreo = await _repositoryApis.PostEndPoint(objEnviarCorreo, uriEnpoint, nombreEnpoint);

                                        return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("200"), StatusCode = "200", Succeeded = true };
                                    }
                                    else
                                        return new ResponseType<string>() { Data = null, Message = "No se pudo actualizar la contraseña", StatusCode = "201", Succeeded = false };
                                }
                                else
                                {
                                    return new ResponseType<string>() { Data = null, Message = "No se pudo actualizar la contraseña", StatusCode = "201", Succeeded = false };
                                }
                            }
                            else
                            {
                                return new ResponseType<string>() { Data = null, Message = "No se pudo actualizar la contraseña en Ecommerce", StatusCode = "201", Succeeded = false };
                            }
                        }
                    }
                    else
                    {
                        return new ResponseType<string>() { Data = null, Message = "Contraseña actual incorrecta", StatusCode = "201", Succeeded = false };
                    }
                }
                else
                {
                    return new ResponseType<string>() { Data = null, Message = "No se pudo validar la contraseña", StatusCode = "201", Succeeded = false };
                }
            }
            catch (Exception ex)
            {
                _log.LogError(string.Empty, ex);
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }
}
