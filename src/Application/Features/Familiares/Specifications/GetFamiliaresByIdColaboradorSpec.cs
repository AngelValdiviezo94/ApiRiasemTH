using Ardalis.Specification;
using EnrolApp.Domain.Entities.Familiares;

namespace EnrolApp.Application.Features.Familiares.Specifications
{
    public class GetFamiliaresByIdColaboradorSpec: Specification<FamiliarColaborador>
    {
        public GetFamiliaresByIdColaboradorSpec(Guid colaboradorId)
        {
            Query.Where(p => p.ColaboradorId == colaboradorId && !p.Eliminado)
                .Include(p => p.ImagenPerfil);
        }
    }
}
