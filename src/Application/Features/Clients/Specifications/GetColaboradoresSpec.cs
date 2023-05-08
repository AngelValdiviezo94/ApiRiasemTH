using Ardalis.Specification;
using EnrolApp.Domain.Entities.Common;

namespace EnrolApp.Application.Features.Clients.Specifications
{
    public class GetColaboradoresSpec : Specification<Cliente>
    {
        public GetColaboradoresSpec()
        {
            Query.Where(p => p.Estado == p.Estado)
                .Include(p => p.ImagenPerfil);
                //.Include(p => p.Cargo);
        }
    }
}