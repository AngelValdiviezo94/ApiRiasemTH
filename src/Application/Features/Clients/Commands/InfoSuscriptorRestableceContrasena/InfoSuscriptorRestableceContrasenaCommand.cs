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

namespace EnrolApp.Application.Features.Clients.Commands.InfoSuscriptorRestableceContrasena
{
    public record InfoSuscriptorRestableceContrasenaCommand(string Identificacion, bool enviaOtp, string tipoColaborador) : IRequest<ResponseType<SuscriptorRestableceContrasenaType>>;

    public class InfoSuscriptorRestableceContrasenaCommandHandler : IRequestHandler<InfoSuscriptorRestableceContrasenaCommand, ResponseType<SuscriptorRestableceContrasenaType>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsyncCl;
        private readonly IRepositoryAsync<FamiliarColaborador> _repositoryAsyncFamCol;
        private readonly IApisConsumoAsync _repositoryApis;
        private readonly IConfiguration _config;
        private readonly ILogger<InfoSuscriptorRestableceContrasenaCommandHandler> _log;
        private readonly string UrlBaseApiAuth = string.Empty;
        private readonly string UrlBaseApiUtils = string.Empty;
        private string nombreEnpoint = string.Empty;
        private string uriEnpoint = string.Empty;

        public InfoSuscriptorRestableceContrasenaCommandHandler(IRepositoryAsync<Cliente> repository, IConfiguration config,
            IApisConsumoAsync repositoryApis, ILogger<InfoSuscriptorRestableceContrasenaCommandHandler> log, IRepositoryAsync<FamiliarColaborador> repositoryAsyncFamCol)
        {
            _repositoryAsyncCl = repository;
            _repositoryAsyncFamCol = repositoryAsyncFamCol;
            _repositoryApis = repositoryApis;
            _config = config;
            _log = log;
            UrlBaseApiAuth = _config.GetSection("ConsumoApis:UrlBaseApiAuth").Get<string>();
            UrlBaseApiUtils = _config.GetSection("ConsumoApis:UrlBaseApiUtils").Get<string>();
        }

        public async Task<ResponseType<SuscriptorRestableceContrasenaType>> Handle(InfoSuscriptorRestableceContrasenaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var info = new SuscriptorRestableceContrasenaType();
                string correo = string.Empty;
                string celular = string.Empty;
                string tipoColaborador = string.IsNullOrEmpty(request.tipoColaborador) ? string.Empty : request.tipoColaborador.ToUpper();

                if (tipoColaborador == "F")
                {
                    var familiar = await _repositoryAsyncFamCol.FirstOrDefaultAsync(new GetFamiliarColaboradorByIdentificacionSpec(request.Identificacion), cancellationToken);

                    if (familiar is null)
                        return new ResponseType<SuscriptorRestableceContrasenaType>() { Succeeded = false, Data = null, Message = "Identificación no registrada", StatusCode = "001" };

                    if (familiar.Estado == "I")
                        return new ResponseType<SuscriptorRestableceContrasenaType>() { Succeeded = false, Data = null, Message = "Familiar no se encuentra suscrito", StatusCode = "001" };

                    if (!familiar.Habilitado)
                        return new ResponseType<SuscriptorRestableceContrasenaType>() { Succeeded = false, Data = null, Message = "Familiar no se encuentra habilitado", StatusCode = "001" };

                    info = new SuscriptorRestableceContrasenaType()
                    {
                        Id = familiar.Id,
                        Nombre = familiar.Alias,
                        Identificacion = familiar.Identificacion,
                        Correo = familiar.Correo,
                        Celular = familiar.Celular
                    };
                }
                else
                {
                    var suscriptor = await _repositoryAsyncCl.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(request.Identificacion), cancellationToken);

                    if (suscriptor is null)
                        return new ResponseType<SuscriptorRestableceContrasenaType>() { Succeeded = false, Data = null, Message = "Identificación no registrada", StatusCode = "001" };

                    if (suscriptor.Estado == "I")
                        return new ResponseType<SuscriptorRestableceContrasenaType>() { Succeeded = false, Data = null, Message = "Colaborador no se encuentra suscrito", StatusCode = "201" };

                    info = new SuscriptorRestableceContrasenaType()
                    {
                        Id = suscriptor.Id,
                        Nombre = suscriptor.Alias,
                        Identificacion = suscriptor.Identificacion,
                        Correo = suscriptor.Correo,
                        Celular = suscriptor.Celular
                    };
                }

                correo = info.Correo;
                celular = info.Celular;

                var correoSuscriptor = info.Correo.Split("@");
                var desCorreo = correoSuscriptor[0];
                var coPro = string.Concat(desCorreo[..3], new string('*', desCorreo.Length).ToString(), "@", correoSuscriptor[1]);

                var celularSuscriptor = info.Celular;
                var celPro = string.Concat(new string('*', 6), celularSuscriptor[6..]);

                info.Correo = request.enviaOtp ? coPro : info.Correo;
                info.Celular = celPro;

                if (request.enviaOtp)
                {
                    #region Generar OTP
                    var objGenOtp = new
                    {
                        codigo = string.Empty,
                        cedula = info.Identificacion
                    };

                    nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:GenerarOtp").Get<string>();
                    uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
                    var (Success, ObjResult) = await _repositoryApis.PostEndPoint(objGenOtp, uriEnpoint, nombreEnpoint);
                    #endregion

                    if (Success)
                    {
                        var codOtp = ((ResponseType<string>)ObjResult).Data;
                        var objEnviarOtp = new
                        {
                            otp = codOtp,
                            celular = celular,
                            alias = info.Nombre,
                            para = correo,
                            plantilla = _config.GetSection("RestableceContrasena:PlantillaEnvioOtp").Get<string>() ?? string.Empty,
                            asunto = _config.GetSection("RestableceContrasena:AsuntoRestablecerContrasena").Get<string>() ?? string.Empty
                        };

                        nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo").Get<string>();
                        uriEnpoint = UrlBaseApiUtils + nombreEnpoint;
                        var resultCorreo = await _repositoryApis.PostEndPoint(objEnviarOtp, uriEnpoint, nombreEnpoint);

                        #region Envio Otp Sms
                        var objEnviarSms = new
                        {
                            celular = celular,
                            messageId = _config.GetSection("Sms:Plantilla:RestablecerContrasenaOtp").Get<string>(),
                            dataVariable = new string[] { info.Nombre, codOtp },
                            identificacion = info.Identificacion
                        };

                        nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarSms").Get<string>();
                        uriEnpoint = UrlBaseApiUtils + nombreEnpoint;
                        var resultSms = await _repositoryApis.PostEndPoint(objEnviarSms, uriEnpoint, nombreEnpoint);
                        #endregion
                    }
                }

                return new ResponseType<SuscriptorRestableceContrasenaType>() { Succeeded = true, Data = info, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000" };
            }
            catch (Exception e)
            {
                _log.LogError(e, string.Empty);
                return new ResponseType<SuscriptorRestableceContrasenaType>() { Succeeded = false, Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500" };
            }
        }
    }
}