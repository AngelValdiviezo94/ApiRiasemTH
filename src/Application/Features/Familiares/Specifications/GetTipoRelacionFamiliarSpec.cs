using Ardalis.Specification;
using EnrolApp.Domain.Entities.Familiares;

namespace EnrolApp.Application.Features.Familiares.Specifications
{
    public class GetTipoRelacionFamiliarSpec : Specification<TipoRelacionFamiliar>
    {
        public GetTipoRelacionFamiliarSpec()
        {
            Query.Where(p => p.Estado == "A")
                .OrderBy(p => p.Nombre);
        }
    }
}
