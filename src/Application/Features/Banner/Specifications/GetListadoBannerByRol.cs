using Ardalis.Specification;
using EnrolApp.Domain.Entities.Marketing;

namespace EnrolApp.Application.Features.Banner.Specifications
{
    public class GetListadoBannerByRol: Specification<RolContenidoMK>
    {
        public GetListadoBannerByRol(Guid uidRol, DateTime fecha)
        {
            Query.Where(rco => rco.RolId == uidRol &&
                               fecha.Date >= rco.ContenidoMK.FechaPublicacion.Date &&
                               fecha.Date <= rco.ContenidoMK.FechaCaducidad.Date &&
                               rco.Estado == "A" && rco.ContenidoMK.Estado == "A")
                .Include(con => con.ContenidoMK);
        }
    }
}