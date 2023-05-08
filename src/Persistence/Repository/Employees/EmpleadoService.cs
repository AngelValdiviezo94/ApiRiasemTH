using EnrolApp.Application.Features.Employees.Interfaces;
using EnrolApp.Domain.Entities.Nomina;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Domain.Entities.Organizacion;
using EnrolApp.Application.Features.Employees.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Application.Features.Clients.Specifications;

namespace EnrolApp.Persistence.Repository.Employees;

public class EmpleadoService : IEmpleado
{
    private readonly ILogger<EmpleadoService> _log;
    private readonly IConfiguration _config;
    private string NombreStoreProcedure = null;
    private readonly string Esquema = null;
    private readonly IRepositoryAsync<CargoEje> _repositoryCargoEje;
    private readonly IRepositoryAsync<Cliente> _repoCliente;
    private string ConnectionString { get; }

    public EmpleadoService(ILogger<EmpleadoService> log, IConfiguration config, IRepositoryAsync<CargoEje> repositoryCargoEje, IRepositoryAsync<Cliente> repoCliente)
    {
      
        _log = log;
        _config = config;
        ConnectionString = _config.GetConnectionString("Bd_Rrhh_Panacea");
        Esquema = _config.GetSection("StoredProcedure:Esquema").Get<string>();
        _repositoryCargoEje = repositoryCargoEje;
        _repoCliente = repoCliente;
    }

    public async Task<InformacionGeneralEmpleado> GetInfoGeneralByIdentificacion(string Identificacion)
    {
        //Consultamos si es usuario Eje
        InformacionGeneralEmpleado objResult = new();
        var clientCargoEje = await _repositoryCargoEje.FirstOrDefaultAsync(new GetCargoEjeSpec(Identificacion));
        if (clientCargoEje != null)
        {
            var cliente = await _repoCliente.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(Identificacion));
            objResult = new()
            {
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                Correo = cliente.Correo,
                Identificacion = Identificacion,
                TipoIdentificacion = cliente.TipoIdentificacion,
                Empresa = clientCargoEje.CargoSG.Departamento.Area.Empresa.RazonSocial,
                Cargo = clientCargoEje.CargoSG.Nombre,
                Area = clientCargoEje.CargoSG.Departamento.Area.Nombre,
                CodigoEmpleado = 0,
                Cod_Empresa = clientCargoEje.CargoSG.Departamento.Area.Empresa.Codigo,
                RucEmpresa = clientCargoEje.CargoSG.Departamento.Area.Empresa.Ruc,
                Sueldo = (decimal)0.0,
                GrupoEmpresarial = clientCargoEje.CargoSG.Departamento.Area.Empresa.GrupoEmpresarial.Nombre,
                TipoContrato = "",
                FechaIngreso = DateTime.Now


            };
        }
        else
        {
            NombreStoreProcedure = _config.GetSection("StoredProcedure:Empleado:InfoGeneralEmpleado").Get<string>();
            using IDbConnection con = new SqlConnection(ConnectionString);
            if (con.State == ConnectionState.Closed) con.Open();
            objResult = await con.QueryFirstOrDefaultAsync<InformacionGeneralEmpleado>(sql: (Esquema + NombreStoreProcedure), param: new { Identificacion }, commandType: CommandType.StoredProcedure);

            if (con.State == ConnectionState.Open) con.Close();
        }
       


        return objResult;
    }

}
