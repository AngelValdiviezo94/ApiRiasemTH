using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Familiares.Commands.CreateFamiliarColaborador;
using EnrolApp.Application.Features.Familiares.Commands.UpdateFamiliarColaborador;
using EnrolApp.Application.Features.Familiares.Dto;
using EnrolApp.Application.Features.Familiares.Interfaces;
using EnrolApp.Application.Features.Familiares.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Familiares;
using EnrolApp.Domain.Entities.Wallet;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace EnrolApp.Persistence.Repository.Familiares
{
    public class FamiliaresService : IFamiliares
    {
        private readonly IRepositoryAsync<TipoRelacionFamiliar> _repoAsyncTipRel;
        private readonly IRepositoryAsync<FamiliarColaborador> _repoAsyncFamCol;
        private readonly IRepositoryAsync<Cliente> _repoAsyncCli;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly string UrlBaseApiEcommerce = string.Empty;
        private readonly string fotoPerfilDefecto;

        public FamiliaresService(IRepositoryAsync<TipoRelacionFamiliar> repoAsyncTipRel, IMapper mapper, IRepositoryAsync<FamiliarColaborador> repoAsyncFamCol, IRepositoryAsync<Cliente> repoAsyncCli, IConfiguration config)
        {
            _repoAsyncTipRel = repoAsyncTipRel;
            _repoAsyncFamCol = repoAsyncFamCol;
            _repoAsyncCli = repoAsyncCli;
            _mapper = mapper;
            _config = config;
            UrlBaseApiEcommerce = _config.GetSection("ConsumoApis:UrlBaseApiEcommerce").Get<string>();
            fotoPerfilDefecto = _config.GetSection("Imagenes:FotoPerfilDefecto").Get<string>();
        }

        public async Task<ResponseType<string>> CreateFamiliarColaboradorAsync(CreateFamiliarColaboradorRequest request, string authToken)
        {
            try
            {
                if (request.Identificacion == request.IdentificacionColaborador)
                    return new ResponseType<string>() { Data = null, Message = "Un colaborador no puede registrarse a sí mismo como familiar", StatusCode = "101", Succeeded = false };

                var familiarColaborador = await _repoAsyncFamCol.FirstOrDefaultAsync(new GetFamiliarColaboradorByIdentificacionSpec(request.Identificacion));

                if (familiarColaborador != null)
                {
                    var colaborador = await _repoAsyncCli.GetByIdAsync(familiarColaborador.ColaboradorId);

                    return new ResponseType<string>() { Data = null, Message = string.Concat("Familiar ya se encuentra asociado a ", colaborador.Apellidos, " ", colaborador.Nombres), StatusCode = "101", Succeeded = false };
                }

                var familiares = await _repoAsyncFamCol.ListAsync(new GetFamiliaresByIdColaboradorSpec(request.ColaboradorId));

                var cupoFamiliares = familiares.Select(p => p.Cupo).Sum();

                var resCupo = await ValidaCupoFamiliarColaboradorAsync(authToken, cupoFamiliares, request.Cupo, "101");

                if (!resCupo.Succeeded)
                    return resCupo;

                var familiar = new FamiliarColaborador()
                {
                    Id = Guid.NewGuid(),
                    ColaboradorId = request.ColaboradorId,
                    Apellidos = request.Apellidos,
                    Nombres = request.Nombres,
                    Alias = request.Alias,
                    Identificacion = request.Identificacion,
                    TipoIdentificacion = request.TipoIdentificacion,
                    Celular = request.Celular,
                    Correo = request.Correo,
                    ServicioActivo = false,
                    Estado = "I",
                    Habilitado = request.Habilitado,
                    Eliminado = false,
                    Cupo = request.Cupo,
                    FechaDesde = DateTime.Parse(request.FechaDesde),
                    FechaHasta = !string.IsNullOrEmpty(request.FechaHasta) ? DateTime.Parse(request.FechaHasta) : null,
                    TipoRelacionFamiliarId = request.TipoRelacionFamiliarId,
                    UsuarioCreacion = request.IdentificacionColaborador,
                    FechaCreacion = DateTime.Now,
                    UsuarioModificacion = string.Empty,
                    SesionColaborador = authToken
                };

                var response = await _repoAsyncFamCol.AddAsync(familiar);

                if (response == null)
                    return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("101"), StatusCode = "101", Succeeded = false };

                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("100"), StatusCode = "100", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

        public async Task<ResponseType<List<ResponseFamiliarColaboradorType>>> GetListadoFamiliarColaboradorAsync(string ColaboradorId)
        {
            try
            {
                var familiares = await _repoAsyncFamCol.ListAsync(new GetFamiliaresByIdColaboradorSpec(Guid.Parse(ColaboradorId)));
                var tipoRelacion = await _repoAsyncTipRel.ListAsync(new GetTipoRelacionFamiliarSpec());

                var lstFamiliares = _mapper.Map<List<ResponseFamiliarColaboradorType>>(familiares);

                foreach (var fam in lstFamiliares)
                {
                    var familiar = familiares.FirstOrDefault(p => p.Id == fam.Id);
                    fam.FotoPerfil = familiar.ImagenPerfil is not null ? familiar.ImagenPerfil.RutaAcceso : fotoPerfilDefecto;
                    fam.TipoRelacionFamiliarDes = tipoRelacion.FirstOrDefault(x => x.Id == fam.TipoRelacionFamiliarId).Nombre;
                }

                return new ResponseType<List<ResponseFamiliarColaboradorType>>() { Data = lstFamiliares, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<List<ResponseFamiliarColaboradorType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
        

        public async Task<ResponseType<List<ResponseFamiliarColaboradorType>>> GetInfoFamiliarColaboradorAsync(string identificacionFamiliar)
        {
            try
            {
                var familiares = await _repoAsyncFamCol.ListAsync(new GetFamiliarColaboradorByIdentificacionSpec(identificacionFamiliar));

                if (familiares is null) return new ResponseType<List<ResponseFamiliarColaboradorType>>() { Data = null, Message = "No se encuentra información.", StatusCode = "000", Succeeded = true };

                var lstFamiliar = _mapper.Map<List<ResponseFamiliarColaboradorType>>(familiares);

                lstFamiliar[0].FotoPerfil = familiares[0].ImagenPerfil?.RutaAcceso is not null ? familiares[0].ImagenPerfil?.RutaAcceso : fotoPerfilDefecto;

                return new ResponseType<List<ResponseFamiliarColaboradorType>>() { Data = lstFamiliar, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<List<ResponseFamiliarColaboradorType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

        
        public async Task<ResponseType<List<ResponseTipoRelacionFamiliarType>>> GetTipoRelacionFamiliarAsync()
        {
            try
            {
                var tipoRelacion = await _repoAsyncTipRel.ListAsync(new GetTipoRelacionFamiliarSpec());

                var lstTipoRelacion = _mapper.Map<List<ResponseTipoRelacionFamiliarType>>(tipoRelacion);

                return new ResponseType<List<ResponseTipoRelacionFamiliarType>>() { Data = lstTipoRelacion, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<List<ResponseTipoRelacionFamiliarType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

        public async Task<ResponseType<string>> UpdateFamiliarColaboradorAsync(UpdateFamiliarColaboradorRequest request, string authToken)
        {
            try
            {
                var familiar = await _repoAsyncFamCol.GetByIdAsync(request.Id);

                if (familiar == null)
                    return new ResponseType<string>() { Data = null, Message = "No se pudo encontrar información del familiar", StatusCode = "201", Succeeded = false };

                var familiarColaborador = await _repoAsyncFamCol.FirstOrDefaultAsync(new GetFamiliarColaboradorByIdentificacionSpec(request.Identificacion));

                if (familiarColaborador != null)
                {
                    if (familiarColaborador.ColaboradorId != request.ColaboradorId)
                    {
                        var colaborador = await _repoAsyncCli.GetByIdAsync(familiarColaborador.ColaboradorId);

                        return new ResponseType<string>() { Data = null, Message = string.Concat("Familiar ya se encuentra asociado a ", colaborador.Apellidos, " ", colaborador.Nombres), StatusCode = "201", Succeeded = false };
                    }

                    if (familiarColaborador.Id != request.Id)
                        return new ResponseType<string>() { Data = null, Message = "Identificación ya se encuentra registrada", StatusCode = "201", Succeeded = false };
                }

                var familiares = await _repoAsyncFamCol.ListAsync(new GetFamiliaresByIdColaboradorSpec(request.ColaboradorId));

                familiares = familiares.Where(x => x.Id != request.Id).ToList();

                var cupoFamiliares = familiares.Select(p => p.Cupo).Sum();

                var resCupo = await ValidaCupoFamiliarColaboradorAsync(authToken, cupoFamiliares, request.Cupo, "201");

                if (!resCupo.Succeeded)
                    return resCupo;

                if (!string.IsNullOrEmpty(request.Nombres) && familiar.Estado == "I")
                    familiar.Nombres = request.Nombres;

                if (!string.IsNullOrEmpty(request.Apellidos) && familiar.Estado == "I")
                    familiar.Apellidos = request.Apellidos;

                if (!string.IsNullOrEmpty(request.Alias) && familiar.Estado == "I")
                    familiar.Alias = request.Alias;

                if (!string.IsNullOrEmpty(request.TipoIdentificacion) && familiar.Estado == "I")
                    familiar.TipoIdentificacion = request.TipoIdentificacion;

                if (!string.IsNullOrEmpty(request.Identificacion) && familiar.Estado == "I")
                    familiar.Identificacion = request.Identificacion;

                if (!string.IsNullOrEmpty(request.Celular) && familiar.Estado == "I")
                    familiar.Celular = request.Celular;

                if (!string.IsNullOrEmpty(request.Correo) && familiar.Estado == "I")
                    familiar.Correo = request.Correo;

                if (familiar.Habilitado != request.Habilitado)
                    familiar.Habilitado = request.Habilitado;

                if (request.Cupo > 0)
                    familiar.Cupo = request.Cupo;

                if (!string.IsNullOrEmpty(request.FechaDesde))
                    familiar.FechaDesde = DateTime.Parse(request.FechaDesde);

                if (!string.IsNullOrEmpty(request.FechaDesde))
                    familiar.FechaHasta = DateTime.Parse(request.FechaHasta);

                if (!string.IsNullOrEmpty(request.TipoRelacionFamiliarId) && familiar.Estado == "I")
                    familiar.TipoRelacionFamiliarId = Guid.Parse(request.TipoRelacionFamiliarId);

                if (request.Eliminado)
                    familiar.Eliminado = request.Eliminado;

                familiar.UsuarioModificacion = request.IdentificacionColaborador;
                familiar.FechaModificacion = DateTime.Now;
                familiar.SesionColaborador = authToken;

                await _repoAsyncFamCol.UpdateAsync(familiar);

                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("200"), StatusCode = "200", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

        public async Task<ResponseType<string>> ValidaCupoFamiliarColaboradorAsync(string authToken, double cupoFamiliares, double cupoRequest, string codeMessage)
        {
            var nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiEcommerce:GetCliente").Get<string>();
            var uriEndPoint = UrlBaseApiEcommerce + nombreEnpoint;

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", authToken ?? string.Empty);

                var resEcommerce = await client.GetAsync(uriEndPoint);

                if (!resEcommerce.IsSuccessStatusCode)
                    return new ResponseType<string>() { Data = null, Message = "No se pudo consultar información del eccomerce", StatusCode = codeMessage, Succeeded = false };

                var resCupo = resEcommerce.Content.ReadFromJsonAsync<ResponseType<CupoCredito>>().Result;
                var colaboradorCupo = resCupo.Data;

                if ((cupoFamiliares + cupoRequest) > Convert.ToDouble(colaboradorCupo.CreditoCupo))
                {
                    return new ResponseType<string>() { Data = null, Message = "Cupo insuficiente para asignar al familiar", StatusCode = codeMessage, Succeeded = false };
                }

                if (cupoRequest > Convert.ToDouble(colaboradorCupo.CreditoCupo))
                {
                    return new ResponseType<string>() { Data = null, Message = "Cupo de familiar no puede ser mayor al cupo propio asignado", StatusCode = codeMessage, Succeeded = false };
                }

                return new ResponseType<string>() { Data = null, Message = "", StatusCode = codeMessage, Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = "No se pudo consultar información del eccomerce", StatusCode = "500", Succeeded = false };
            }
        }

        public async Task<ResponseType<string>> UpdateSesionFamiliarAsync(string Identificacion, string Token)
        {
            try
            {
                var colaborador = await _repoAsyncCli.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(Identificacion));
                if (colaborador == null) return new ResponseType<string>() { Data = null, Message = "No se encontró el colaborador", StatusCode = "201", Succeeded = false };

                var familiares = await _repoAsyncFamCol.ListAsync(new GetFamiliaresByIdColaboradorSpec(Guid.Parse(colaborador.Id.ToString())));
                if (familiares.Count == 0) return new ResponseType<string>() { Data = null, Message = "Colaborador no registra familiares", StatusCode = "201", Succeeded = false };

                foreach (var item in familiares)
                {
                    item.SesionColaborador = Token;
                    await _repoAsyncFamCol.UpdateAsync(item);
                }

                return new ResponseType<string>() { Data = null, Message = "Actualizado correctamente", StatusCode = "200", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = "No se pudo consultar información del eccomerce", StatusCode = "500", Succeeded = false };
            }
        }

    }
}
