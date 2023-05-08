using Ardalis.Specification;
using EnrolApp.Domain.Entities.Permisos;

namespace EnrolApp.Application.Features.Notificacions.Specifications
{
    public class SolicitudPermisoByMotivoSpec : Specification<SolicitudPermiso>
    {
        public SolicitudPermisoByMotivoSpec(Guid idTipoPermiso, DateTime FechaDesde, DateTime FechaHasta, Guid EstadoSolicitada)
        {
            Query.Where(x => x.IdTipoPermiso == idTipoPermiso &&
                        x.FechaCreacion.Date >= FechaDesde.Date &&
                        x.FechaCreacion.Date <= FechaHasta.Date &&
                        x.IdEstadoSolicitud == EstadoSolicitada)
               .Include(x => x.TipoPermiso);
        }
    }
}