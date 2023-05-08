using Ardalis.Specification;
using EnrolApp.Domain.Entities.Marketing;

namespace EnrolApp.Application.Features.Banner.Specifications
{
    public class GetContenidoByIdSpec: Specification<ContenidoCategoriaMK>
    {
        public GetContenidoByIdSpec(Guid uidContenido)
        {
            Query.Where(cca => cca.ContenidoId == uidContenido && cca.Estado == "A")
                .Include(cat => cat.Categoria)
                .Include(cca => cca.Contenido)
                .ThenInclude(tco => tco.TipoContenido);
        }
    }
}
