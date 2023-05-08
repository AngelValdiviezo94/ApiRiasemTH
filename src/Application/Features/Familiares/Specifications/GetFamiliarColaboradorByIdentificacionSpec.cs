using Ardalis.Specification;
using EnrolApp.Domain.Entities.Familiares;

namespace EnrolApp.Application.Features.Familiares.Specifications
{
    public class GetFamiliarColaboradorByIdentificacionSpec: Specification<FamiliarColaborador>
    {
        public GetFamiliarColaboradorByIdentificacionSpec(string identificacion)
        {
            Query.Where(p => p.Identificacion == identificacion && !p.Eliminado)
                .Include(p => p.ImagenPerfil);
        }
    }
}
