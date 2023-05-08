using Ardalis.Specification;
using EnrolApp.Domain.Entities.Common;

namespace EnrolApp.Application.Features.Clients.Specifications
{
    public class GetColaboradorConvivenciaByIdentificacionSpec : Specification<ColaboradorConvivencia>
    {
        public GetColaboradorConvivenciaByIdentificacionSpec(string Identificacion)
        {
            Query.Where(x => x.Identificacion == Identificacion).Take(1);
        }
    }
}
