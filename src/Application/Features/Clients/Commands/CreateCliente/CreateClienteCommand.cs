using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Prospectos.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Familiares;
using EnrolApp.Domain.Entities.Suscripcion;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace EnrolApp.Application.Features.Clients.Commands.CreateCliente;

public record CreateClienteCommand(CreateClienteRequest ClienteRequest) : IRequest<ResponseType<string>>;

public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<FamiliarColaborador> _repoFamiColAsync;
    private readonly IRepositoryAsync<Cliente> _repoClieAsync;
    private readonly IRepositoryAsync<Prospecto> _repoProspAsync;
    private readonly IRepositoryAsync<ColaboradorConvivencia> _repositoryCCAsync;
    private readonly IMapper _mapper;
    private readonly IApisConsumoAsync _repositoryApis;
    private readonly IConfiguration _config;
    private readonly ILogger<CreateClienteCommandHandler> _log;
    private readonly string UrlBaseApiAuth = "";
    private readonly string UrlBaseApiUtils = "";
    private readonly string UrlBaseApiEcommerce = "";
    private readonly string UrlBaseApiBuenDia = string.Empty;
    private readonly string UrlBaseApiEvalCore = string.Empty;
    private readonly string Esquema = string.Empty;
    private string nombreEnpoint = "";
    private string uriEnpoint = "";
    private string NombreStoreProcedure = string.Empty;
    private string urlBaseApiUtils = string.Empty;
    private string featureCredenciaId = string.Empty;
    private string ConnectionString { get; }

    public CreateClienteCommandHandler(IRepositoryAsync<FamiliarColaborador> repositoryFamiCol, IRepositoryAsync<Cliente> repository, IRepositoryAsync<Prospecto> repoProspAsync, IApisConsumoAsync repositoryApis,
        IConfiguration config, IMapper mapper, ILogger<CreateClienteCommandHandler> log, IRepositoryAsync<ColaboradorConvivencia> repositoryCCAsync)
    {
        _repoFamiColAsync = repositoryFamiCol;
        _repoClieAsync = repository;
        _repoProspAsync = repoProspAsync;
        _repositoryApis = repositoryApis;
        _repositoryCCAsync = repositoryCCAsync;
        _mapper = mapper;
        _config = config;
        _log = log;
        UrlBaseApiUtils = _config.GetSection("ConsumoApis:UrlBaseApiUtils").Get<string>();
        UrlBaseApiAuth = _config.GetSection("ConsumoApis:UrlBaseApiAuth").Get<string>();
        UrlBaseApiEcommerce = _config.GetSection("ConsumoApis:UrlBaseApiEcommerce").Get<string>();
        UrlBaseApiBuenDia = _config.GetSection("ConsumoApis:UrlBaseApiBuenDia").Get<string>();
        UrlBaseApiEvalCore = _config.GetSection("ConsumoApis:UrlBaseApiEvalCore").Get<string>();
        ConnectionString = _config.GetConnectionString("Bd_Rrhh_Panacea");
        Esquema = _config.GetSection("StoredProcedure:Esquema").Get<string>();
        urlBaseApiUtils = _config.GetSection("ConsumoApis:UrlBaseApiUtils").Get<string>();
        featureCredenciaId = _config.GetSection("Features:Credencial").Get<string>();
    }

    public async Task<ResponseType<string>> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //request.ClienteRequest.TipoCliente = "C";
            var objClient = _mapper.Map<Cliente>(request.ClienteRequest);

            #region procesamiento de adjunto

            Guid? uidAdjuntoNuevo = null;
            Guid? uidFacialPersona = null;

            if (request.ClienteRequest.Adjunto != null)
            {
                var objAdjunto = new Adjunto
                {
                    Identificacion = request.ClienteRequest.Identificacion,
                    FechaCreacion = DateTime.Now,
                    IdSolicitud = null,
                    IdFeature = featureCredenciaId,
                    IdTipoSolicitud = null,
                    NombreArchivo = string.Concat(Guid.NewGuid(), ".", request.ClienteRequest.Adjunto.Extension),
                    ArchivoBase64 = request.ClienteRequest.Adjunto.Base64,
                    ExtensionArchivo = string.Concat(".", request.ClienteRequest.Adjunto.Extension),
                };

                nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:Adjuntos").Get<string>();
                uriEnpoint = urlBaseApiUtils + nombreEnpoint;

                var (Success, Data) = await _repositoryApis.PostEndPoint(objAdjunto, uriEnpoint, nombreEnpoint);

                if (Success)
                {
                    var resp = (ResponseType<string>)Data;
                    uidAdjuntoNuevo = Guid.Parse(resp.Data);

                    Cliente cliente = null;

                    if (request.ClienteRequest.TipoCliente == "C")
                        cliente = await _repoClieAsync.FirstOrDefaultAsync(new ClienteByIdentificacionRelacionadoSpec(request.ClienteRequest.TipoIdentificacion, request.ClienteRequest.Identificacion), cancellationToken);

                    var objFacial = new
                    {
                        colaborador = request.ClienteRequest.Identificacion,
                        base64 = request.ClienteRequest.Adjunto.Base64,
                        nombre = request.ClienteRequest.Adjunto.Nombre,
                        extension = request.ClienteRequest.Adjunto.Extension,
                        facialPersonUid = cliente is not null ? cliente.FacialPersonId : null
                    };

                    nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiEvalCore:CreacionPersona").Get<string>();
                    uriEnpoint = UrlBaseApiEvalCore + nombreEnpoint;

                    var (SuccessEval, DataEval) = await _repositoryApis.PostEndPoint(objFacial, uriEnpoint, nombreEnpoint);

                    if (DataEval != null)
                    {
                        var respEval = (ResponseType<string>)DataEval;

                        if (respEval.Succeeded)
                            uidFacialPersona = Guid.Parse(respEval.Data);
                        else
                            return new ResponseType<string>() { Message = "No se pudo registrar la información de la imagen", StatusCode = "101", Succeeded = false };
                    }
                    else
                    {
                        return new ResponseType<string>() { Message = "No se pudo registrar la información de la imagen", StatusCode = "101", Succeeded = false };
                    }
                }
            }
            #endregion


            if (request.ClienteRequest.TipoCliente.ToUpper() == "F")
            {

                var objFamiliarO = _mapper.Map<Cliente>(request.ClienteRequest);
                var familiar = await _repoFamiColAsync.FirstOrDefaultAsync(new FamiliarByIdentificacionSpec(request.ClienteRequest.TipoIdentificacion, request.ClienteRequest.Identificacion), cancellationToken);
                if (familiar is null) return new ResponseType<string>() { Message = "No se encuentra el familiar.", StatusCode = "101", Succeeded = true };


                familiar.Direccion = objFamiliarO.Direccion;
                familiar.Correo = objFamiliarO.Correo;
                familiar.ServicioActivo = true;
                familiar.Genero = objFamiliarO.Genero;
                familiar.Latitud = objFamiliarO.Latitud;
                familiar.Longitud = objFamiliarO.Longitud;
                familiar.UsuarioCreacion = "SYSTEM";
                familiar.Estado = "A";
                familiar.FechaCreacion = DateTime.UtcNow;
                familiar.ImagenPerfilId = uidAdjuntoNuevo;
                familiar.FechaNacimiento = objFamiliarO.FechaNacimiento;

                await _repoFamiColAsync.UpdateAsync(familiar, cancellationToken);


                var resLdapFamiliar = await ActualizaLdap(familiar.Identificacion, request.ClienteRequest.Direccion, familiar.Celular, request.ClienteRequest.Correo, request.ClienteRequest.Password);

                if (!resLdapFamiliar.Success) return new ResponseType<string>() { Message = "Ocurrió un errror al actualizar el familiar en el Directorio", StatusCode = "101", Succeeded = true };

                var objEnviar = new
                {
                    para = familiar.Correo,
                    alias = familiar.Alias,
                    identificacion = familiar.Identificacion,
                    plantilla = _config.GetSection("Notificaciones:PlantillaActivaServicio").Get<string>() ?? string.Empty,
                    asunto = _config.GetSection("Notificaciones:AsuntoActivaServicio").Get<string>() ?? string.Empty
                };

                nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo").Get<string>();
                uriEnpoint = UrlBaseApiUtils + nombreEnpoint;
                await _repositoryApis.PostEndPoint(objEnviar, uriEnpoint, nombreEnpoint);

                return new ResponseType<string>() { Data = familiar.Id.ToString(), Message = "Cliente registrado exitosamente", StatusCode = "000", Succeeded = true };

            }
            else
            {
                #region Consulta información empleado base Panacea(RRHH)
                var infoEmp = await _repositoryCCAsync.FirstOrDefaultAsync(new GetColaboradorConvivenciaByIdentificacionSpec(objClient.Identificacion), cancellationToken);
                //NombreStoreProcedure = _config.GetSection("StoredProcedure:Empleado:InfoGeneralEmpleado").Get<string>();

                //using IDbConnection con = new SqlConnection(ConnectionString);

                //if (con.State == ConnectionState.Closed) con.Open();
                //var infoEmp = await con.QueryFirstOrDefaultAsync<InformacionGeneralEmpleado>(sql: (Esquema + NombreStoreProcedure), param: new { objClient.Identificacion }, commandType: CommandType.StoredProcedure);

                //if (con.State == ConnectionState.Open) con.Close();
                #endregion

                #region Validar si existe usuario / mover a ecommerce
                var nombreEnpointQuery = _config.GetSection("EndPointConsumoApis:ApiBuenDia:GetClienteQuery").Get<string>();
                var uriEnpointQuery = UrlBaseApiBuenDia + nombreEnpointQuery;

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("ClientSecret", "sic-inventory");
                client.DefaultRequestHeaders.Add("ClientId", "inventory-app");
                client.DefaultRequestHeaders.Add("CurrentOrganizationId", "1");
                var response = await client.GetAsync(string.Concat(uriEnpointQuery, request.ClienteRequest.Identificacion), cancellationToken: cancellationToken);
                var clientesSebeliResponse = JsonConvert.DeserializeObject<List<ClienteSebeli>>(response.Content.ReadAsStringAsync(cancellationToken).Result);
                var clienteSebeli = clientesSebeliResponse.Count > 0 ? clientesSebeliResponse[0] : null;
                #endregion

                if (clienteSebeli is null) return new ResponseType<string>() { Message = "Para registrarte como cliente, debes estar registrado en Sebeli", StatusCode = "101", Succeeded = true };

                var objClientResul = await _repoClieAsync.FirstOrDefaultAsync(new ClienteByIdentificacionRelacionadoSpec(request.ClienteRequest.TipoIdentificacion, request.ClienteRequest.Identificacion), cancellationToken);


                if (objClientResul is null)
                {
                    Cliente objNewClient = new()
                    {
                        CodigoIntegracion = clienteSebeli is not null ? clienteSebeli.IdCliente.ToString() : string.Empty,
                        CodigoConvivencia = infoEmp is not null ? infoEmp.CodigoBiometrico : string.Empty,
                        //Alias = infoEmp.Nombres.Split(" ")[0], /*objProspecto.Alias*/
                        TipoIdentificacion = objClient.TipoIdentificacion,
                        //Nombres = infoEmp.Nombres,
                        //Apellidos = infoEmp?.Apellidos,
                        DispositivoId = objClient.DispositivoId,
                        FechaNacimiento = objClient.FechaNacimiento,
                        Latitud = objClient.Latitud,
                        Longitud = objClient.Longitud,
                        //Celular = objClient?.Celular;
                        Correo = objClient.Correo,
                        Direccion = objClient.Direccion,
                        Genero = objClient.Genero,
                        TipoIdentificacionFamiliar = objClient?.TipoIdentificacionFamiliar,
                        IndentificacionFamiliar = objClient.IndentificacionFamiliar,
                        ServicioActivo = false,
                        Estado = "A",
                        ImagenPerfilId = uidAdjuntoNuevo,
                        FacialPersonId = uidFacialPersona
                    };

                    var resultCliente = await _repoClieAsync.AddAsync(objNewClient, cancellationToken);
                }
                else
                {
                    //objClient.Id = /*Guid.NewGuid();*/
                    objClientResul.CodigoIntegracion = clienteSebeli is not null ? clienteSebeli.IdCliente.ToString() : string.Empty;
                    objClientResul.CodigoConvivencia = infoEmp is not null ? infoEmp.CodigoBiometrico : string.Empty;
                    //objClientResul.Alias = infoEmp.Nombres.Split(" ")[0]; /*objProspecto.Alias*/
                    objClientResul.TipoIdentificacion = objClient.TipoIdentificacion;
                    //objClientResul.Nombres = infoEmp.Nombres;
                    //objClientResul.Apellidos = infoEmp?.Apellidos;
                    objClientResul.DispositivoId = objClient.DispositivoId;
                    objClientResul.FechaNacimiento = objClient.FechaNacimiento;
                    objClientResul.Latitud = objClient.Latitud;
                    objClientResul.Longitud = objClient.Longitud;
                    //objClientResul.Celular = objClient?.Celular;
                    objClientResul.Correo = objClient.Correo;
                    objClientResul.Direccion = objClient.Direccion;
                    objClientResul.Genero = objClient.Genero;
                    objClientResul.TipoIdentificacionFamiliar = objClient?.TipoIdentificacionFamiliar;
                    objClientResul.IndentificacionFamiliar = objClient.IndentificacionFamiliar;
                    objClientResul.ServicioActivo = false;
                    objClientResul.Estado = "A";
                    //objClientResul.CargoId = Guid.Parse(_config.GetSection("CargoPorDefecto:CargoUidSuscriptor").Get<string>() ?? string.Empty);
                    objClientResul.ImagenPerfilId = uidAdjuntoNuevo;
                    objClientResul.FacialPersonId = uidFacialPersona;

                    await _repoClieAsync.UpdateAsync(objClientResul, cancellationToken);
                }

                //SE ACTUALIZA EN LDAP

                var resLdap = await ActualizaLdap(objClientResul.Identificacion, objClient.Direccion, objClient.Celular, objClient.Correo, objClient.Password);
                if (!resLdap.Success) return new ResponseType<string>() { Message = resLdap.Message, StatusCode = "102", Succeeded = false };

                var objUpdateUserBuenDia = new
                {
                    LogonName = objClient.Identificacion,
                    Password = objClient.Password,
                    ConfirmPassword = string.Empty
                };

                var nombreEnpointRecoverPassword = _config.GetSection("EndPointConsumoApis:ApiBuenDia:RecoveryPassword").Get<string>();
                var uriEnpointRecoverPassword = UrlBaseApiBuenDia + nombreEnpointRecoverPassword;

                using var clientRecoverPassword = new HttpClient();
                clientRecoverPassword.DefaultRequestHeaders.Accept.Clear();
                var responseRecoverPassword = await clientRecoverPassword.PostAsJsonAsync(uriEnpointRecoverPassword, objUpdateUserBuenDia, cancellationToken: cancellationToken);
                var buendiaResponse = JsonConvert.DeserializeObject<RecoveryPassword>(responseRecoverPassword.Content.ReadAsStringAsync(cancellationToken).Result);

                if (!buendiaResponse.IsValid)
                {
                    return new ResponseType<string>() { Message = "Error al integrar Ecosistema con Ecommerce", Succeeded = false, StatusCode = "101" };
                }

                var objEnviar = new
                {
                    para = objClient.Correo,
                    alias = objClientResul?.Alias,
                    identificacion = objClient.Identificacion,
                    plantilla = _config.GetSection("Notificaciones:PlantillaActivaServicio").Get<string>() ?? string.Empty,
                    asunto = _config.GetSection("Notificaciones:AsuntoActivaServicio").Get<string>() ?? string.Empty
                };

                nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo").Get<string>();
                uriEnpoint = UrlBaseApiUtils + nombreEnpoint;
                await _repositoryApis.PostEndPoint(objEnviar, uriEnpoint, nombreEnpoint);

                return new ResponseType<string>() { Data = objClientResul.Id.ToString(), Message = "Cliente registrado exitosamente", StatusCode = "000", Succeeded = true };
            }
            #region Se comenta
            //var objValiUserLdap = new
            //{
            //    identificacion = request.ClienteRequest.Identificacion,
            //};

            //nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ValidarUsuarioLdap").Get<string>();
            //uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
            //var objData = await _repositoryApis.PostEndPoint(objValiUserLdap, uriEnpoint, nombreEnpoint);

            //if (objData.Success)
            //{
            //    //Actualizo datos de usuario en LDAP
            //    var objUpdateUser = new
            //    {
            //        identificacion = objClient.Identificacion,
            //        direccion = objClient.Direccion,
            //        telefono = objClient.Celular,
            //        correo = objClient.Correo,
            //        password = objClient.Password
            //    };

            //    nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ActualizarUsuarioLdap").Get<string>();
            //    uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
            //    var objDataUpdate = await _repositoryApis.PutEndPoint(objUpdateUser, uriEnpoint, nombreEnpoint);
            //    if (!objDataUpdate.Success)
            //    {
            //        return new ResponseType<string>() { Message = "Error al generar la contraseña", StatusCode = "101", Succeeded = false };
            //    }
            //}
            //else
            //{
            //    //Crear Nuevo Usuario en LDAP
            //    var objNewUser = new
            //    {
            //        identificacion = objClient.Identificacion,
            //        nombres = objClient.Nombres,
            //        apellidos = objClient?.Apellidos,
            //        codigoempresa = infoEmp is not null ? infoEmp.CodUdn : string.Empty,
            //        grupoempresarial = "Riasem", //objClient.Cargo.Departamento.Area.Empresa.GrupoEmpresarial.Codigo,
            //        empresa = infoEmp is not null ? infoEmp.DesUdn : string.Empty,
            //        area = infoEmp is not null ? infoEmp.DesArea : string.Empty,
            //        departamento = infoEmp is not null ? infoEmp.DesSubcentroCosto : string.Empty,
            //        direccion = objClient.Direccion,
            //        fecharegistro = DateTime.Now,
            //        telefono = objClient.Celular,
            //        correo = objClient.Correo,
            //        password = objClient.Password
            //    };
            //    nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:CrearUsuarioLdap").Get<string>();
            //    uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
            //    var objDataUpdate = await _repositoryApis.PostEndPoint(objNewUser, uriEnpoint, nombreEnpoint);
            //}

            #endregion


            //if (objResult is not null)
            //{


            //    if (objData.Success)
            //    {
            //        nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiEcommerce:CreateCliente").Get<string>();
            //        uriEnpoint = UrlBaseApiEcommerce + nombreEnpoint;
            //        var objClientEcom = new
            //        {
            //            IdEmpresa = 1,
            //            IdTipoIdentificacion = objClient.TipoIdentificacion == "C" ? 1 : 3,
            //            Identificacion = objClient.Identificacion,
            //            Descripcion = objClient.Nombres + " " + objClient.Apellidos,
            //            RazonSocial = objClient.Nombres + " " + objClient.Apellidos,
            //            CorreoElectronico = objClient.Correo,
            //            CreditoCupo = 10,
            //            CreditoDisponible = 10,
            //            Direccion = objClient.Direccion,
            //            Telefono = objClient.Celular,
            //            Password = objClient.Password,
            //            ConfirmarPassword = objClient.Password,
            //            FechaNacimiento = objClient.FechaNacimiento,
            //            IdAlianza = 3,
            //            Genero = objClient.Genero,
            //            Activo = 1,
            //            IdArea = 1,
            //            IdCanal = 2
            //        };

            //        if (clienteSebeli is null)
            //        {
            //            //Crear Usuario Ecommerce
            //            (bool Success, object Data) = await _repositoryApis.PostEndPoint(objClientEcom, uriEnpoint, nombreEnpoint);
            //        }
            //        else
            //        {
            //            var objUpdateUserBuenDia = new
            //            {
            //                LogonName = objClient.Identificacion,
            //                Password = objClient.Password,
            //                ConfirmPassword = string.Empty
            //            };

            //            var nombreEnpointRecoverPassword = _config.GetSection("EndPointConsumoApis:ApiBuenDia:RecoveryPassword").Get<string>();
            //            var uriEnpointRecoverPassword = UrlBaseApiBuenDia + nombreEnpointRecoverPassword;

            //            using var clientRecoverPassword = new HttpClient();
            //            clientRecoverPassword.DefaultRequestHeaders.Accept.Clear();
            //            var responseRecoverPassword = await clientRecoverPassword.PostAsJsonAsync(uriEnpointRecoverPassword, objUpdateUserBuenDia, cancellationToken: cancellationToken);
            //            var buendiaResponse = JsonConvert.DeserializeObject<RecoveryPassword>(responseRecoverPassword.Content.ReadAsStringAsync(cancellationToken).Result);
            //        }

            //        //if (Success)
            //        //{
            //        var objEnviar = new
            //        {
            //            para = objClient.Correo,
            //            alias = objProspecto?.Alias,
            //            identificacion = objProspecto.Identificacion,
            //            plantilla = _config.GetSection("Notificaciones:PlantillaActivaServicio").Get<string>() ?? string.Empty,
            //            asunto = _config.GetSection("Notificaciones:AsuntoActivaServicio").Get<string>() ?? string.Empty
            //        };

            //        nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo").Get<string>();
            //        uriEnpoint = UrlBaseApiUtils + nombreEnpoint;
            //        await _repositoryApis.PostEndPoint(objEnviar, uriEnpoint, nombreEnpoint);
            //        //}
            //    }
            //    else
            //        return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("102"), StatusCode = "102", Succeeded = false };
            //}
            //else
            //    return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("102"), StatusCode = "102", Succeeded = false };

            //return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Cliente registrado exitosamente", StatusCode = "000", Succeeded = true };
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
        }
    }

    private async Task<(bool Success, string Message)> ActualizaLdap(string identificacion, string direccion, string telefono, string correo, string password)
    {
        var objValiUserLdap = new
        {
            identificacion,
        };

        nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ValidarUsuarioLdap").Get<string>();
        uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
        var (Success, Data) = await _repositoryApis.PostEndPoint(objValiUserLdap, uriEnpoint, nombreEnpoint);

        if (Success)
        {
            //Actualizo datos de usuario en LDAP
            var objUpdateUser = new
            {
                identificacion,
                direccion,
                telefono,
                correo,
                password
            };

            nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ActualizarUsuarioLdap").Get<string>();
            uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
            var objDataUpdate = await _repositoryApis.PutEndPoint(objUpdateUser, uriEnpoint, nombreEnpoint);
            if (!objDataUpdate.Success)
            {
                return (false, "No se ha podido actualizar en el directorio");
            }
        }
        else
        {
            return (false, "No se ha podido actualizar en el directorio");
            #region se comenta
            //Crear Nuevo Usuario en LDAP
            //var objNewUser = new
            //{
            //    identificacion = objClient.Identificacion,
            //    nombres = objClient.Nombres,
            //    apellidos = objClient?.Apellidos,
            //    codigoempresa = infoEmp is not null ? infoEmp.CodUdn : string.Empty,
            //    grupoempresarial = "Riasem", //objClient.Cargo.Departamento.Area.Empresa.GrupoEmpresarial.Codigo,
            //    empresa = infoEmp is not null ? infoEmp.DesUdn : string.Empty,
            //    area = infoEmp is not null ? infoEmp.DesArea : string.Empty,
            //    departamento = infoEmp is not null ? infoEmp.DesSubcentroCosto : string.Empty,
            //    direccion = objClient.Direccion,
            //    fecharegistro = DateTime.Now,
            //    telefono = objClient.Celular,
            //    correo = objClient.Correo,
            //    password = objClient.Password
            //};
            //nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:CrearUsuarioLdap").Get<string>();
            //uriEnpoint = UrlBaseApiAuth + nombreEnpoint;
            //var objDataUpdate = await _repositoryApis.PostEndPoint(objNewUser, uriEnpoint, nombreEnpoint);

            #endregion
        }
        return (true, "Se ha actualizado correctamente");
    }
}
