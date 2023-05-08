using Dapper;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Horarios.Commands.CreateMarcacion;
using EnrolApp.Application.Features.Horarios.Dto;
using EnrolApp.Application.Features.Horarios.Interfaces;
using EnrolApp.Application.Features.Horarios.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Horario;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Net.Http.Json;

namespace EnrolApp.Persistence.Repository.Employees;

public class HorarioService : IHorario
{
    private readonly ILogger<HorarioService> _log;
    private readonly IConfiguration _config;
    private readonly IRepositoryMarcacionesAsync<UserInfo> _repoUserInfoAsync;
    private readonly IRepositoryMarcacionesAsync<CheckInOut> _repoCheckInOutAsync;
    private readonly IRepositoryAsync<Cliente> _repositoryClient;
    private readonly IRepositoryAsync<TurnoEnrol> _repositoryTurnoEnrol;
    private readonly IApisConsumoAsync _repositoryApis;
    private readonly string Esquema = null;
    private string ConnectionString { get;}
    private string ConnectionString_Marc { get;}
    private readonly string UrlBaseApiEvalCore = string.Empty;
    private string nombreEnpoint = "";
    private string uriEnpoint = "";
    private readonly string UrlBaseApiEcommerce = "";

    public HorarioService(ILogger<HorarioService> log, IConfiguration config, IRepositoryMarcacionesAsync<UserInfo> repoUserInfoAsync, IApisConsumoAsync repositoryApis,
        IRepositoryMarcacionesAsync<CheckInOut> repoCheckInOutAsync, IRepositoryAsync<Cliente> repositoryClient, IRepositoryAsync<TurnoEnrol> repositoryTurnoEnrol)
    {
        _log = log;
        _config = config;
        ConnectionString = _config.GetConnectionString("Bd_Rrhh_Panacea");
        ConnectionString_Marc = _config.GetConnectionString("Bd_Marcaciones_GRIAMSE");
        Esquema = _config.GetSection("StoredProcedure:Esquema").Get<string>();
        UrlBaseApiEcommerce = _config.GetSection("ConsumoApis:UrlBaseApiEvalCore").Get<string>();
        nombreEnpoint = "Turnos/GetTurnosAsignados";
        _repoCheckInOutAsync = repoCheckInOutAsync;
        _repoUserInfoAsync = repoUserInfoAsync;
        _repositoryClient = repositoryClient;
        _repositoryTurnoEnrol = repositoryTurnoEnrol;
        UrlBaseApiEvalCore = "";
        _repositoryApis = repositoryApis;
    }


    public async Task<List<Horario>> GetHorarioByFilterAsync(string Identificacion, string FechaDesde, string FechaHasta, string Token)
    {
        List<TurnoColaboradorType> resulTask2 = new();
        List<TurnoColaboradorType> turnoFiltrado = new();
        var ObjClientPadre = await _repositoryClient.ListAsync();
        var ObjClient = await _repositoryClient.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(Identificacion));

        string identificacionClientPadre = ObjClientPadre.Where(e => e.Id == ObjClient.ClientePadreId).Select(e => e.Identificacion).FirstOrDefault();

        //var objTurnosEnrol = await _repositoryTurnoEnrol.ListAsync();

        //var objTurnosFiltrados = objTurnosEnrol.Where(e => e.IdColaborador == ObjClient.Id).ToList();

        if (!string.IsNullOrEmpty(identificacionClientPadre))
        {
            string uri = UrlBaseApiEcommerce + nombreEnpoint + "?identificacion=" + identificacionClientPadre + "&fechaDesde=" + FechaDesde + "&fechaHasta=" + FechaHasta;
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var response = await client.GetAsync(uri);
            var local = response.Content.ReadAsStringAsync().Result;

            //resulTask = response.Content.ReadAsStringAsync().Result;
            resulTask2 = response.Content.ReadFromJsonAsync<ResponseType<List<TurnoColaboradorType>>>().Result.Data;
            turnoFiltrado = resulTask2.Where(e => e.IdColaborador == ObjClient.Id).ToList();

            //Se agrega filtro turno padre
            turnoFiltrado = turnoFiltrado.Where(x => x.IdTurnoPadre == null).ToList();
        }


        List<Horario> objHorario = new();

        for (var dt = DateTime.Parse(FechaDesde); dt <= DateTime.Parse(FechaHasta); dt = dt.AddDays(1))
        {
            objHorario.Add(new Horario
            {
                Fecha = dt
            });
        }

        var TurnosEmpleado = _config.GetSection("StoredProcedure:HorariosEmpleado:TurnoAsignadoEmpleado").Get<string>();
        var MarcacionesEmpleado = _config.GetSection("StoredProcedure:HorariosEmpleado:MarcacionesEmpleado").Get<string>();
        var fechaHastaMarc = "";

        using IDbConnection con = new SqlConnection(ConnectionString);
        //if (con.State == ConnectionState.Closed) con.Open();
        //var objTurnos = await con.QueryAsync<Turno>(sql: (Esquema + TurnosEmpleado), param: new { Identificacion, FechaDesde, FechaHasta }, commandType: CommandType.StoredProcedure);
        //if (con.State == ConnectionState.Open) con.Close();

        fechaHastaMarc = DateTime.Parse(FechaHasta).AddDays(1).ToString("yyyy/MM/dd");

        using IDbConnection con_marc = new SqlConnection(ConnectionString_Marc);
        if (con_marc.State == ConnectionState.Closed) con_marc.Open();
        var objMarcaciones = await con_marc.QueryAsync<Marcacion>(sql: (Esquema + MarcacionesEmpleado), param: new { Identificacion, FechaDesde, fechaHastaMarc }, commandType: CommandType.StoredProcedure);
        if (con.State == ConnectionState.Open) con.Close();


        //recorrido de objHorario junto a turnos y marcaciones
        //var result = objHorario.Select(e => {
        //    var ret = e;
        //    //Turnos
        //    e.IdTurno = objTurnos.Where(f => f.Fecha.Value.Date == e.Fecha.Value.Date).Select(f => f.IdTurno).FirstOrDefault();
        //    e.TurnoEntrada = objTurnos.Where(f => f.Fecha.Value.Date == e.Fecha.Value.Date).Select(f => f.TurnoEntrada).FirstOrDefault();
        //    e.TurnoSalida = objTurnos.Where(f => f.Fecha.Value.Date == e.Fecha.Value.Date).Select(f => f.TurnoSalida).FirstOrDefault();
        //    e.Receso = objTurnos.Where(f => f.Fecha.Value.Date == e.Fecha.Value.Date).Select(f => f.Receso).FirstOrDefault();
        //    e.CodigoTurno = objTurnos.Where(f => f.Fecha.Value.Date == e.Fecha.Value.Date).Select(f => f.CodigoTurno.ToString()).FirstOrDefault();
        //    e.TipoTurno = objTurnos.Where(f => f.Fecha.Value.Date == e.Fecha.Value.Date).Select(f => f.TipoTurno.ToString()).FirstOrDefault();
        //    e.DescripcionTurno = objTurnos.Where(f => f.Fecha.Value.Date == e.Fecha.Value.Date).Select(f => f.DescripcionTurno.ToString()).FirstOrDefault();


        //    //Marcaciones
        //    e.IdMarcacionE = objMarcaciones.Where(m => m.CheckMarcacion.Value.Date == e.Fecha.Value.Date && m.TipoMarcacion == 10).Select(m => m.IdMarcacion).FirstOrDefault();
        //    e.IdMarcacionS = objMarcaciones.Where(m => m.CheckMarcacion.Value.Date == e.Fecha.Value.Date && m.TipoMarcacion == 11).Select(m => m.IdMarcacion).FirstOrDefault();
        //    e.MarcacionEntrada = objMarcaciones.Where(m => m.CheckMarcacion.Value.Date == e.Fecha.Value.Date && m.TipoMarcacion == 10).Select(m => m.CheckMarcacion).FirstOrDefault();
        //    e.MarcacionSalida = objMarcaciones.Where(m => m.CheckMarcacion.Value.Date == e.Fecha.Value.Date && m.TipoMarcacion == 11).Select(m => m.CheckMarcacion).FirstOrDefault();
        //    e.MarcacionEntradaReceso = objMarcaciones.Where(m => m.CheckMarcacion.Value.Date == e.Fecha.Value.Date && m.TipoMarcacion == 14).Select(m => m.CheckMarcacion).FirstOrDefault();
        //    e.MarcacionSalidaReceso = objMarcaciones.Where(m => m.CheckMarcacion.Value.Date == e.Fecha.Value.Date && m.TipoMarcacion == 15).Select(m => m.CheckMarcacion).FirstOrDefault();
        //    e.CodigoBiometrico = objMarcaciones.Select(m => m.CodigoBiometrico.ToString()).FirstOrDefault();

        //    return e;

        //});

        //recorrido de objHorario junto a turnos y marcaciones
        
            
        var resulta = objHorario.Select(e => {
            var ret = e;
            //Turnos
            e.IdTurno = turnoFiltrado.Where(f => f.FechaAsignacion.Date == e.Fecha.Value.Date).Select(f => f.IdTurno).FirstOrDefault();
            e.TurnoEntrada = turnoFiltrado.Where(f => f.FechaAsignacion.Date == e.Fecha.Value.Date).Select(f => f.HoraEntrada).FirstOrDefault();
            e.TurnoSalida = turnoFiltrado.Where(f => f.FechaAsignacion.Date == e.Fecha.Value.Date).Select(f => f.HoraSalida).FirstOrDefault();
            //e.Receso = turnoFiltrado.Where(f => f.FechaAsignacion == e.Fecha.Value.Date).Select(f => f.tot).FirstOrDefault();
            e.CodigoTurno = turnoFiltrado.Where(f => f.FechaAsignacion.Date == e.Fecha.Value.Date).Select(f => f.CodigoTurno.ToString()).FirstOrDefault();
            e.TipoTurno = "LABORAL";
            //e.TipoTurno = turnoFiltrado.Where(f => f.FechaAsignacion == e.Fecha.Value.Date).Select(f => f.TipoTurno.ToString()).FirstOrDefault();
            e.DescripcionTurno = turnoFiltrado.Where(f => f.FechaAsignacion.Date == e.Fecha.Value.Date).Select(f => f.CodigoTurno).FirstOrDefault();


            //Marcaciones
            e.IdMarcacionE = objMarcaciones.Where(m => m.CheckMarcacion.Value.Date == e.Fecha.Value.Date && m.TipoMarcacion == 10).OrderBy(m => m.CheckMarcacion).Select(m => m.IdMarcacion).FirstOrDefault();
            e.IdMarcacionS = objMarcaciones.Where(m => m.CheckMarcacion.Value.Date == e.Fecha.Value.Date && m.TipoMarcacion == 11).OrderByDescending(m => m.CheckMarcacion).Select(m => m.IdMarcacion).FirstOrDefault();
            e.MarcacionEntrada = objMarcaciones.Where(m => m.CheckMarcacion.Value.Date == e.Fecha.Value.Date && m.TipoMarcacion == 10).Select(m => m.CheckMarcacion).FirstOrDefault();
            e.MarcacionSalida = objMarcaciones.Where(m => m.CheckMarcacion.Value.Date == e.Fecha.Value.Date && m.TipoMarcacion == 11).OrderByDescending(m => m.CheckMarcacion).Select(m => m.CheckMarcacion).FirstOrDefault();
            e.MarcacionEntradaReceso = objMarcaciones.Where(m => m.CheckMarcacion.Value.Date == e.Fecha.Value.Date && m.TipoMarcacion == 14).Select(m => m.CheckMarcacion).FirstOrDefault();
            e.MarcacionSalidaReceso = objMarcaciones.Where(m => m.CheckMarcacion.Value.Date == e.Fecha.Value.Date && m.TipoMarcacion == 15).OrderByDescending(m => m.CheckMarcacion).Select(m => m.CheckMarcacion).FirstOrDefault();
            e.CodigoBiometrico = objMarcaciones.Select(m => m.CodigoBiometrico.ToString()).FirstOrDefault();

            return e;

        });
      


        return (List<Horario>)resulta.ToList();
    }


    public async Task<string> CreateMarcacion(CreateMarcacionRequest Request, CancellationToken cancellationToken)
    {
        try
        {
            var userInfo = await _repoUserInfoAsync.FirstOrDefaultAsync(new UserMarcacionByCodigoSpec(Request.CodigoEmpleado), cancellationToken);
            var ObjClient = await _repositoryClient.FirstOrDefaultAsync(new ClienteByCodigoSpec(Request.CodigoEmpleado, ""), cancellationToken);

            //CONSULTA SI ID LOCALIDAD SE ENCUENTRA ASIGNADA

            if (userInfo is null)
            {
                UserInfo objUserInfo = new()
                {
                    Badgenumber = Request.CodigoEmpleado,
                    Ssn = ObjClient.Identificacion,
                    Name = ObjClient.Nombres,
                    LastName = ObjClient.Apellidos,
                    DefaultDeptId = 0, //int.Parse(ObjClient.Cargo.Departamento.Codigo),
                    CreateOperator = "Admin",
                    CreateTime = DateTime.Now
                };

                userInfo = await _repoUserInfoAsync.AddAsync(objUserInfo, cancellationToken);

            }
            if(Request.DispositivoId == ObjClient.DispositivoId)
            {
                var countMarcacion = await _repoCheckInOutAsync.CountAsync(new MarcacionByUserIdSpec(userInfo.UserId), cancellationToken);


                CheckInOut entityCheck = new()
                {
                    UserId = userInfo.UserId,
                    CheckType = countMarcacion == 0 ? "I" : "O",
                    Sn = Request.DispositivoId
                };

                using IDbConnection con = new SqlConnection(ConnectionString_Marc);
                if (con.State == ConnectionState.Closed) con.Open();
                var objResult = await con.ExecuteAsync(sql: (" INSERT INTO [GRIAMSE].[dbo].[CHECKINOUT] (USERID,CHECKTYPE) VALUES (" + entityCheck.UserId + ",'" + entityCheck.CheckType + "')"), commandType: CommandType.Text);
                con.Close();
                return objResult.ToString();
            }

            return "El dispositivo por el que desea registrar su marcación es incorrecto.";
            
        }
        catch (Exception ex)
        {

            return ex.Message;
        }


    }


}
