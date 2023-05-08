using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Prospectos.Dto;
using EnrolApp.Application.Features.Prospectos.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Familiares;
using EnrolApp.Domain.Entities.Organizacion;
using EnrolApp.Domain.Entities.Suscripcion;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EnrolApp.Application.Features.Prospectos.Queries.GetProspectoByIdentificacion;

public record GetProspectoByIdentificacionQuery(string TipoProspecto, string TipoIdentificacion, string Identificacion) : IRequest<ResponseType<ProspectoType>>;

public class GetProspectoByIdentificacionQueryHandler : IRequestHandler<GetProspectoByIdentificacionQuery, ResponseType<ProspectoType>>
{
    private readonly IRepositoryAsync<Prospecto> _repositoryAsync;
    private readonly IRepositoryAsync<Cliente> _repositoryClAsync;
    private readonly IRepositoryAsync<FamiliarColaborador> _repositoryFamiAsync;
    private readonly IRepositoryAsync<ColaboradorConvivencia> _repositoryCCAsync;
    private readonly IRepositoryAsync<CargoEje> _repositoryAsyncCargoEje;
    private readonly IApisConsumoAsync _repositoryApis;
    private readonly IConfiguration _config;
    private readonly ILogger<GetProspectoByIdentificacionQueryHandler> _log;
    private readonly string UrlBaseApiUtils = "";
    private readonly string UrlBaseApiAuth = "";
    private readonly string UrlBaseApiBuenDia = string.Empty;
    private string nombreEnpoint = "";
    private string uriEnpoint = "";

    public GetProspectoByIdentificacionQueryHandler(IRepositoryAsync<FamiliarColaborador> repositoryFamiAsync, IRepositoryAsync<Prospecto> repositoryAsync, IRepositoryAsync<Cliente> repositoryClAsync, IRepositoryAsync<CargoEje> repositoryAsyncCargoEje,
        IApisConsumoAsync repositoryApis, IConfiguration config, ILogger<GetProspectoByIdentificacionQueryHandler> log, IRepositoryAsync<ColaboradorConvivencia> repositoryCCAsync)
    {
        _repositoryAsync = repositoryAsync;
        _repositoryFamiAsync = repositoryFamiAsync;
        _repositoryClAsync = repositoryClAsync;
        _repositoryApis = repositoryApis;
        _repositoryCCAsync = repositoryCCAsync;
        _repositoryAsyncCargoEje = repositoryAsyncCargoEje;
        _config = config;
        _log = log;
        UrlBaseApiUtils = _config.GetSection("ConsumoApis:UrlBaseApiUtils").Get<string>();
        UrlBaseApiAuth = _config.GetSection("ConsumoApis:UrlBaseApiAuth").Get<string>();
        UrlBaseApiBuenDia = _config.GetSection("ConsumoApis:UrlBaseApiBuenDia").Get<string>();
    }

    public async Task<ResponseType<ProspectoType>> Handle(GetProspectoByIdentificacionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            ProspectoType objProspecto = new();
            //consulto por el tipo de prospecto
            if (request.TipoProspecto.ToUpper() == "F") //familiar
            {
                #region Valido si existe el familiar
                var objFamiliar = await _repositoryFamiAsync.FirstOrDefaultAsync(new FamiliarByIdentificacionSpec(request.TipoIdentificacion.ToUpper(),request.Identificacion), cancellationToken);
                if (objFamiliar is null) return new ResponseType<ProspectoType>() { Message = "No existe el familiar", StatusCode = "001", Succeeded = true };

                if (objFamiliar.ServicioActivo || objFamiliar.Estado == "A") return new ResponseType<ProspectoType>() { Message = "El familiar ya se encuentra suscrito", StatusCode = "001", Succeeded = true };

                if (objFamiliar.Eliminado) return new ResponseType<ProspectoType>() { Message = "No se encuentra el familiar", StatusCode = "001", Succeeded = true };

                if (!objFamiliar.Habilitado  /*||(objFamiliar.FechaDesde < DateTime.Now || objFamiliar.FechaHasta > DateTime.Now)*/) return new ResponseType<ProspectoType>() { Message = "Familiar no autorizado", StatusCode = "001", Succeeded = true };

                var objColFamiliar = await _repositoryClAsync.GetByIdAsync(objFamiliar.ColaboradorId, cancellationToken);

                objProspecto = new ProspectoType()
                {
                    Id = objFamiliar.Id,
                    TipoIdentificacion = objFamiliar.TipoIdentificacion,
                    Identificacion = objFamiliar.Identificacion,
                    Nombres = objFamiliar.Nombres,
                    Alias = objFamiliar.Alias,
                    Apellidos = objFamiliar.Apellidos,
                    Celular = objFamiliar.Celular,
                    GrupoEmpresarial = "Riasem",
                    Empresa = "LAFATTORIA S.A.",
                    CodigoEmpresa = "01",
                    Area = "FAMILIAR",
                    Departamento = "FAMILIAR",
                    Email = objFamiliar.Correo ?? string.Empty
                };
                #endregion
            }
            else
            {

                #region Validar si existe usuario / mover a ecommerce
                var nombreEnpointQuery = _config.GetSection("EndPointConsumoApis:ApiBuenDia:GetClienteQuery").Get<string>();
                var uriEnpointQuery = UrlBaseApiBuenDia + nombreEnpointQuery;

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("ClientSecret", "sic-inventory");
                client.DefaultRequestHeaders.Add("ClientId", "inventory-app");
                client.DefaultRequestHeaders.Add("CurrentOrganizationId", "1");
                var response = await client.GetAsync(string.Concat(uriEnpointQuery, request.Identificacion), cancellationToken: cancellationToken);
                var clientesSebeliResponse = JsonConvert.DeserializeObject<List<ClienteSebeli>>(response.Content.ReadAsStringAsync(cancellationToken).Result);
                var clienteSebeli = clientesSebeliResponse.Count > 0 ? clientesSebeliResponse[0] : null;
                #endregion

                if (clienteSebeli is null) return new ResponseType<ProspectoType>() { Message = "Para continuar con la suscripción, debes ser autorizado", StatusCode = "001", Succeeded = true };

                var entityCliente = await _repositoryClAsync.FirstOrDefaultAsync(new ClienteByIdentificacionRelacionadoSpec(request.TipoIdentificacion.ToUpper(), request.Identificacion), cancellationToken);

                var clientEje = await _repositoryAsyncCargoEje.FirstOrDefaultAsync(new GetClientesEjesSpec(string.Empty, string.Empty, string.Empty, request.Identificacion));

                var cc = await _repositoryCCAsync.FirstOrDefaultAsync(new GetColaboradorConvivenciaByIdentificacionSpec(request.Identificacion), cancellationToken);


                if (entityCliente is not null)
                {
                    #region Verifica si es Cliente
                    if (entityCliente.Estado == "I")
                    {
                        objProspecto = new ProspectoType()
                        {
                            Id = entityCliente.Id,
                            TipoIdentificacion = entityCliente.TipoIdentificacion,
                            Identificacion = entityCliente.Identificacion,
                            Nombres = entityCliente.Nombres,
                            Alias = entityCliente.Alias,
                            Apellidos = entityCliente.Apellidos,
                            Celular = entityCliente.Celular,
                            GrupoEmpresarial = "Riasem",
                            Empresa = clientEje is not null ? clientEje.CargoSG.Departamento.Area.Empresa.RazonSocial : cc is not null ? cc.DesUdn : string.Empty,
                            CodigoEmpresa = clientEje is not null ? clientEje.CargoSG.Departamento.Area.Empresa.Codigo : cc is not null ? cc.CodUdn : string.Empty,
                            Area = clientEje is not null ? clientEje.CargoSG.Departamento.Area.Nombre : cc is not null ? cc.DesArea : string.Empty,
                            Departamento = clientEje is not null ? clientEje.CargoSG.Departamento.Nombre : cc is not null ? cc.DesSubcentroCosto : string.Empty,
                            Email = entityCliente.Correo ?? string.Empty
                        };
                    }
                    else if (entityCliente.ServicioActivo)
                        return new ResponseType<ProspectoType>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Cliente ya registrado" };
                    else
                        return new ResponseType<ProspectoType>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Cliente pendiente de activación de servicio" };
                    #endregion
                }
                else
                {
                    #region Logica Prospecto
                    var entityProspecto = await _repositoryAsync.FirstOrDefaultAsync(new ProspectoByIdentificacionSpec(request.Identificacion, "P"), cancellationToken);

                    if (entityProspecto is null)
                    {
                        return new ResponseType<ProspectoType>() { Data = null, Succeeded = false, Message = "No existe información para mostrar", StatusCode = "002" };
                    }

                    objProspecto = new ProspectoType()
                    {
                        Id = entityProspecto.Id,
                        TipoIdentificacion = entityProspecto.TipoIdentificacion,
                        Identificacion = entityProspecto.Identificacion,
                        Nombres = entityProspecto.Nombres,
                        Alias = entityProspecto.Alias,
                        Apellidos = entityProspecto.Apellidos,
                        Celular = entityProspecto.Celular,
                        GrupoEmpresarial = "Riasem",
                        Empresa = clientEje is not null ? clientEje.CargoSG.Departamento.Area.Empresa.RazonSocial : cc is not null ? cc.DesUdn : string.Empty,
                        CodigoEmpresa = clientEje is not null ? clientEje.CargoSG.Departamento.Area.Empresa.Codigo : cc is not null ? cc.CodUdn : string.Empty,
                        Area = clientEje is not null ? clientEje.CargoSG.Departamento.Area.Nombre : cc is not null ? cc.DesArea : string.Empty,
                        Departamento = clientEje is not null ? clientEje.CargoSG.Departamento.Nombre : cc is not null ? cc.DesSubcentroCosto : string.Empty,
                        Email = entityProspecto.Email ?? string.Empty
                    };
                    #endregion
                }
            }

            var objUsuarioLdap = new
            {
                Identificacion = objProspecto.Identificacion ?? string.Empty,
                Nombres = objProspecto.Nombres ?? string.Empty,
                Apellidos = objProspecto.Apellidos ?? string.Empty,
                Codigoempresa = objProspecto.CodigoEmpresa ?? string.Empty,
                GrupoEmpresarial = objProspecto.GrupoEmpresarial ?? string.Empty,
                Empresa = objProspecto.Empresa ?? string.Empty,
                Area = objProspecto.Area ?? string.Empty,
                Departamento = objProspecto.Departamento ?? string.Empty
            };

            //valida cliente en LDAP
            nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ValidarUsuarioLdap").Get<string>();
            uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
            var (SuccessValida, ObjResultValida) = await _repositoryApis.PostEndPoint(new { identificacion = objProspecto.Identificacion ?? string.Empty }, uriEnpoint, nombreEnpoint);

            if (!((ResponseType<string>)ObjResultValida).Succeeded)
            {
                nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:CrearUsuarioLdap").Get<string>();
                uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
                var (SuccessAuth, ObjResultAuth) = await _repositoryApis.PostEndPoint(objUsuarioLdap, uriEnpoint, nombreEnpoint);
                if (!SuccessAuth) return new ResponseType<ProspectoType>() { Message = "Ocurrió un error al crear el usuario en el directorio", StatusCode = "001", Succeeded = true };

            }

            var objGenOtp = new
            {
                codigo = string.Empty,
                cedula = objProspecto.Identificacion
            };

            nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:GenerarOtp").Get<string>();
            uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
            var (Success, ObjResult) = await _repositoryApis.PostEndPoint(objGenOtp, uriEnpoint, nombreEnpoint);

            if (Success)
            {
                var codOtp = ((ResponseType<string>)ObjResult).Data;
                var objEnviarOtp = new
                {
                    otp = codOtp,
                    celular = objProspecto.Celular,
                    alias = objProspecto.Alias,
                    para = objProspecto.Email,
                    plantilla = _config.GetSection("Notificaciones:PlantillaOtp").Get<string>() ?? string.Empty,
                    asunto = _config.GetSection("Notificaciones:AsuntoCodigoOtp").Get<string>() ?? string.Empty
                };
                nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo").Get<string>();
                uriEnpoint = UrlBaseApiUtils + nombreEnpoint;
                var resultCorreo = await _repositoryApis.PostEndPoint(objEnviarOtp, uriEnpoint, nombreEnpoint);

                #region Envio Otp Sms
                var objEnviarSms = new
                {
                    celular = objProspecto.Celular,
                    messageId = _config.GetSection("Sms:Plantilla:EnviarOtp").Get<string>(),
                    dataVariable = new string[] { objProspecto.Alias, codOtp },
                    identificacion = objProspecto.Identificacion
                };

                nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarSms").Get<string>();
                uriEnpoint = UrlBaseApiUtils + nombreEnpoint;
                var resultSms = await _repositoryApis.PostEndPoint(objEnviarSms, uriEnpoint, nombreEnpoint);
                #endregion
            }

            return new ResponseType<ProspectoType>() { Data = objProspecto, Succeeded = true, StatusCode = "000", Message = CodeMessageResponse.GetMessageByCode("000") };
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<ProspectoType>() { Data = null, Succeeded = false, StatusCode = "500", Message = e.Message + "   " + e.StackTrace + " ++++++++++++ " + e.Source };
        }
    }
}