using Ardalis.Specification;
using EnrolApp.Domain.Entities.Notificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Notificacions.Specifications
{
    public class NotificacionByIdSolicitudSpec : Specification<BitacoraNotificacion>
    {
        public NotificacionByIdSolicitudSpec(Guid IdSolicitud, Guid UidNotificPorAprobar)
        {
            Query.Where(p => p.SolicitudId == IdSolicitud && p.EventoDifusion.ClasificacionId == UidNotificPorAprobar && p.Estado == "A" && p.EventoDifusion.Estado == "A")
           .Include(p => p.Cliente)
           .Include(ev => ev.EventoDifusion)
           .ThenInclude(evd => evd.Evento)
           .Include(ev => ev.EventoDifusion)
           .ThenInclude(evd => evd.Plantilla)
           .Include(ev => ev.EventoDifusion)
           .ThenInclude(evd => evd.Clasificacion)
           .OrderBy(x => x.EventoDifusion.Clasificacion.Orden)
           .ThenByDescending(x => x.FechaCreacion);
        }
    }
}
