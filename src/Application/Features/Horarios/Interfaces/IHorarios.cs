using EnrolApp.Application.Features.Horarios.Commands.CreateMarcacion;
using EnrolApp.Domain.Entities.Horario;
using EnrolApp.Domain.Entities.Nomina;

namespace EnrolApp.Application.Features.Horarios.Interfaces;

public interface IHorario
{
    /// <summary>
    /// Metodo que genera el reporte de rol de pagos
    /// </summary>
    /// <param name="Identificacion">identificacion del empleado</param>
    /// <param name="FechaDesde">fecha desde</param>
    /// <param name="FechaHasta">fecha hasta</param>
    /// <returns></returns>
    Task<List<Horario>> GetHorarioByFilterAsync(string Identificacion, string FechaDesde, string FechaHasta, string Token);

    Task<string> CreateMarcacion(CreateMarcacionRequest Request, CancellationToken cancellationToken);
}
