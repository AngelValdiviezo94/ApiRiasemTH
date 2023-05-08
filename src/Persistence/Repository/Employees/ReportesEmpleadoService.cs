using Dapper;
using EnrolApp.Application.Features.Employees.Interfaces;
using EnrolApp.Domain.Entities.Nomina;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace EnrolApp.Persistence.Repository.Employees;

public class ReportesEmpleadoService : IReportesEmpleado
{
    private readonly ILogger<ReportesEmpleadoService> _log;
    private readonly IConfiguration _config;
    private string NombreStoreProcedure = null;
    private readonly string Esquema = null;
    private string ConnectionString { get;}

    public ReportesEmpleadoService(ILogger<ReportesEmpleadoService> log, IConfiguration config)
    {
        _log = log;
        _config = config;
        ConnectionString = _config.GetConnectionString("Bd_Rrhh_Panacea");
        Esquema = _config.GetSection("StoredProcedure:Esquema").Get<string>();
    }

   
    public  async Task<CertificadoLaboral> GetCertificadoLaboralByIdentificacionAsync(string Identificacion)
    {
        
        NombreStoreProcedure = _config.GetSection("StoredProcedure:ReportesEmpleado:CertificadoLaboral").Get<string>();
      
        using IDbConnection con = new SqlConnection(ConnectionString);
        if (con.State == ConnectionState.Closed) con.Open();
        var objResult = await con.QueryFirstOrDefaultAsync<CertificadoLaboral>(sql: (Esquema + NombreStoreProcedure), param: new { Identificacion }, commandType: CommandType.StoredProcedure);
        con.Close();

        return objResult;
    }

    public async Task<AvisoEntrada> GetAvisoEntradaByIdentificacionAsync(string Identificacion)
    {
       
        NombreStoreProcedure = _config.GetSection("StoredProcedure:ReportesEmpleado:AvisoEntrada").Get<string>();
        using IDbConnection con = new SqlConnection(ConnectionString);
        if (con.State == ConnectionState.Closed) con.Open();
        var objResult = await con.QueryFirstOrDefaultAsync<AvisoEntrada>(sql: (Esquema + NombreStoreProcedure), param: new { Identificacion }, commandType: CommandType.StoredProcedure);
        if (con.State == ConnectionState.Open) con.Close();
       
        return objResult;
    }


    public async Task<RolPago> GetRolPagoByFilterAsync(string Identificacion,string FechaCorte)
    {
        try
        {
            RolPago objRolPago = new();
            var SpCabecera = _config.GetSection("StoredProcedure:ReportesEmpleado:RolPagoCabecera").Get<string>();
            var SpDetIngEgresos = _config.GetSection("StoredProcedure:ReportesEmpleado:RolPagoDetIngEgreso").Get<string>();
            //FechaCorte 2022-01
            var Anio = Convert.ToInt32(FechaCorte.Split('-')[0]);
            var Mes = Convert.ToInt32(FechaCorte.Split('-')[1]);

            using IDbConnection con = new SqlConnection(ConnectionString);
            if (con.State == ConnectionState.Closed) con.Open();
            var objCabecera = await con.QueryFirstOrDefaultAsync<RolPagoCabecera>(sql: (Esquema + SpCabecera), param: new { Identificacion, Anio, Mes }, commandType: CommandType.StoredProcedure);
            var objDetalle = await con.QueryAsync<Rubro>(sql: (Esquema + SpDetIngEgresos), param: new { Identificacion, Anio, Mes }, commandType: CommandType.StoredProcedure);
            if (con.State == ConnectionState.Open) con.Close();

            objCabecera.EncargadoCorporativoRRHH = "PSIC. MARTHA ZAMBRANO MAESTRE";
            objCabecera.CargoCoporativoRRHH = "GERENTE DE TALENTO HUMANO";

            objRolPago.CabeceraRol = objCabecera;
            objRolPago.ListaIngresos = objDetalle.Where(x => x.TipoRubro == "I").ToList();
            objRolPago.ListaEgresos = objDetalle.Where(x => x.TipoRubro == "D").ToList();

            var TotalIngresos = objDetalle.Where(x => x.TipoRubro == "I").Sum(z => z.Valor);
            var TotalEgresos = objDetalle.Where(x => x.TipoRubro == "D").Sum(z => z.Valor);

            objRolPago.TotalEgresos = TotalEgresos;
            objRolPago.TotalIngresos = TotalIngresos;
            objRolPago.NetoPagar = Math.Round((TotalIngresos - TotalEgresos), 2);

            return objRolPago;
        }
        catch (Exception e)
        {

            throw;
        }
    }



}
