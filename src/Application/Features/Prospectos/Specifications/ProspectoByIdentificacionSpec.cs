using Ardalis.Specification;
using EnrolApp.Domain.Entities.Suscripcion;


namespace EnrolApp.Application.Features.Prospectos.Specifications;

public class ProspectoByIdentificacionSpec : Specification<Prospecto>
{
    public ProspectoByIdentificacionSpec(string Identificacion,string CodTipoRelacion)
    {

        Query.Where(p => p.Identificacion == Identificacion && p.TipoRelacion.Codigo == CodTipoRelacion)
           .Include(p => p.Departamento)
           .ThenInclude(dpto => dpto.Area)
           .ThenInclude(area => area.Empresa)
           .ThenInclude(emp => emp.GrupoEmpresarial);

    }
}
