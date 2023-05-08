using Ardalis.Specification;
using EnrolApp.Domain.Entities.Common;

namespace EnrolApp.Application.Features.Prospectos.Specifications;

public class ClienteByIdentificacionSpec : Specification<Cliente>
{
    public ClienteByIdentificacionSpec(string TipoIdentificacion,string Identificacion)
    {
        Query.Where((p => p.TipoIdentificacion == TipoIdentificacion && p.Identificacion == Identificacion));
            
    }
}