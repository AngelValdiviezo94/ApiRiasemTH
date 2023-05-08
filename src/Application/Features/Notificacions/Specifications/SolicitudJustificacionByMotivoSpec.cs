using Ardalis.Specification;
using EnrolApp.Domain.Entities.Justificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Notificacions.Specifications
{
    public class SolicitudJustificacionByMotivoSpec : Specification<SolicitudJustificacion>
    {
        public SolicitudJustificacionByMotivoSpec(Guid idTipoPermiso, DateTime FechaDesde, DateTime FechaHasta, Guid EstadoSolicitada)
        {
            Query.Where(x => x.IdTipoJustificacion == idTipoPermiso &&
                        x.FechaCreacion.Date >= FechaDesde.Date &&
                        x.FechaCreacion.Date <= FechaHasta.Date &&
                        x.IdEstadoSolicitud == EstadoSolicitada)
               .Include(x => x.TipoJustificacion);
        }
    }
}
