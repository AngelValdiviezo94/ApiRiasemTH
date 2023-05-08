using EnrolApp.Domain.Entities.Nomina;

namespace EnrolApp.Application.Features.Employees.Interfaces;

public interface IReportesEmpleado
{
    /// <summary>
    /// Metodo que obtiene informacion del empleado para generar el 
    /// certificado laboral
    /// </summary>
    /// <param name="Identificacion">identificacion</param>
    /// <returns>objeto con informacion del certificado laboral</returns>
    Task<CertificadoLaboral> GetCertificadoLaboralByIdentificacionAsync(string Identificacion);


    /// <summary>
    /// Metodo que obtiene informacion del empleado para generar el 
    /// aviso de entrada iess
    /// </summary>
    /// <param name="Identificacion">identificacion</param>
    /// <returns>objeto con informacion de aviso entrada</returns>
    Task<AvisoEntrada> GetAvisoEntradaByIdentificacionAsync(string Identificacion);


    /// <summary>
    /// Metodo que genera el reporte de rol de pagos
    /// </summary>
    /// <param name="Identificacion">identificacion del empleado</param>
    /// <param name="FechaCorte">fecha de corte</param>
    /// <returns></returns>
    Task<RolPago> GetRolPagoByFilterAsync(string Identificacion, string FechaCorte);
}
