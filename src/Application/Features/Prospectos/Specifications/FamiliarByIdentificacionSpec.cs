using Ardalis.Specification;
using EnrolApp.Domain.Entities.Familiares;

namespace EnrolApp.Application.Features.Prospectos.Specifications;

public class FamiliarByIdentificacionSpec : Specification<FamiliarColaborador>
{
    public FamiliarByIdentificacionSpec(string TipoIdentificacion,string Identificacion)
    {
        Query.Where((p => p.TipoIdentificacion == TipoIdentificacion && p.Identificacion == Identificacion && !p.Eliminado));
            
    }
}