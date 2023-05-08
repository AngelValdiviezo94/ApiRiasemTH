using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EnrolApp.Application.Features.Clients.Commands.UpdateInfoPersonalColaborador
{
    public record UpdateInfoPersonalColaboradorCommand(ListadoColaboradoresType Request) : IRequest<ResponseType<string>>;

    public class UpdateInfoPersonalColaboradorCommandHandler : IRequestHandler<UpdateInfoPersonalColaboradorCommand, ResponseType<string>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsyncCl;
        private readonly IConfiguration _config;
        private readonly IApisConsumoAsync _repositoryApis;
        private readonly string urlBaseApiUtils = string.Empty;
        private readonly string featureCredenciaId = string.Empty;
        private readonly string UrlBaseApiEvalCore = string.Empty;
        private string nombreEnpoint = string.Empty;
        private string uriEnpoint = string.Empty;

        public UpdateInfoPersonalColaboradorCommandHandler(IRepositoryAsync<Cliente> repository, IConfiguration config, IApisConsumoAsync repositoryApis)
        {
            _repositoryApis = repositoryApis;
            _repositoryAsyncCl = repository;
            _config = config;
            featureCredenciaId = _config.GetSection("Features:Credencial").Get<string>();
            urlBaseApiUtils = _config.GetSection("ConsumoApis:UrlBaseApiUtils").Get<string>();
            UrlBaseApiEvalCore = _config.GetSection("ConsumoApis:UrlBaseApiEvalCore").Get<string>();
        }

        public async Task<ResponseType<string>> Handle(UpdateInfoPersonalColaboradorCommand request, CancellationToken cancellationToken)
        {
            Guid uidAdjuntoNuevo;

            try
            {
                var cliente = await _repositoryAsyncCl.GetByIdAsync(request.Request.Id);

                if (request.Request.Latitud != 0)
                    cliente.Latitud = request.Request.Latitud;

                if (request.Request.Longitud != 0)
                    cliente.Longitud = request.Request.Longitud;

                if (request.Request.Celular != null)
                    cliente.Celular = request.Request.Celular;

                if (request.Request.Correo != null)
                    cliente.Correo = request.Request.Correo;

                if (request.Request.Direccion != null)
                    cliente.Direccion = request.Request.Direccion;

                if (request.Request.Adjunto != null)
                {
                    var objAdjunto = new Adjunto
                    {
                        Identificacion = cliente.Identificacion,
                        FechaCreacion = DateTime.Now,
                        IdSolicitud = null,
                        IdFeature = featureCredenciaId,
                        IdTipoSolicitud = null,
                        NombreArchivo = string.Concat(Guid.NewGuid(), ".", request.Request.Adjunto.Extension),
                        ArchivoBase64 = request.Request.Adjunto.Base64,
                        ExtensionArchivo = string.Concat(".", request.Request.Adjunto.Extension),
                    };

                    nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:Adjuntos").Get<string>();
                    uriEnpoint = urlBaseApiUtils + nombreEnpoint;

                    var (Success, Data) = await _repositoryApis.PostEndPoint(objAdjunto, uriEnpoint, nombreEnpoint);

                    if (Data != null)
                    {
                        var resp = (ResponseType<string>)Data;
                        uidAdjuntoNuevo = Guid.Parse(resp.Data);
                        cliente.ImagenPerfilId = uidAdjuntoNuevo;

                        var objFacial = new
                        {
                            colaborador = cliente.Identificacion,
                            base64 = request.Request.Adjunto.Base64,
                            nombre = request.Request.Adjunto.Nombre,
                            extension = request.Request.Adjunto.Extension,
                            facialPersonUid = cliente is not null ? cliente.FacialPersonId : null
                        };

                        nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiEvalCore:CreacionPersona").Get<string>();
                        uriEnpoint = UrlBaseApiEvalCore + nombreEnpoint;

                        var (SuccessEval, DataEval) = await _repositoryApis.PostEndPoint(objFacial, uriEnpoint, nombreEnpoint);

                        if (DataEval != null)
                        {
                            var respEval = (ResponseType<string>)DataEval;

                            if (respEval.Succeeded)
                                cliente.FacialPersonId = Guid.Parse(respEval.Data);
                            else
                                return new ResponseType<string>() { Message = "No se pudo registrar la información de la imagen", StatusCode = "101", Succeeded = false };
                        }
                        else
                        {
                            return new ResponseType<string>() { Message = "No se pudo registrar la información de la imagen", StatusCode = "101", Succeeded = false };
                        }
                    }
                }

                await _repositoryAsyncCl.UpdateAsync(cliente);

                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("200"), StatusCode = "200", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }
}
