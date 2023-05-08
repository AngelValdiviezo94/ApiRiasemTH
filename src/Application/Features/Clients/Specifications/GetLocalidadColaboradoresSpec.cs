using Ardalis.Specification;
using EnrolApp.Domain.Entities.Horario;

namespace EnrolApp.Application.Features.Clients.Specifications
{
    public class GetLocalidadColaboradoresSpec : Specification<LocalidadColaborador>
    {
        public GetLocalidadColaboradoresSpec(Guid IdColaborador)
        {
            if (IdColaborador == Guid.Empty)
                Query.Where(p => p.Estado == "A");
            else
                Query.Where(p => p.IdColaborador == IdColaborador && p.Estado == "A");
        }
    }
}
