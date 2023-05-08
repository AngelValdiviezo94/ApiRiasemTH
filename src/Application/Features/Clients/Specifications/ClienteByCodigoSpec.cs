using Ardalis.Specification;
using EnrolApp.Domain.Entities.Common;


namespace EnrolApp.Application.Features.Clients.Specifications;

public class ClienteByCodigoSpec : Specification<Cliente>
{
    public ClienteByCodigoSpec(string Codigo, string Identificacion)
    {
        if (string.IsNullOrEmpty(Identificacion))
            Query.Where(p => p.CodigoConvivencia == Codigo);
        //.Include(p => p.Cargo);
        else
            Query.Where(p => p.Identificacion == Identificacion);
    }
}
