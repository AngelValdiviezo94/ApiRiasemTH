using Ardalis.Specification;
using EnrolApp.Domain.Entities.Notificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Notificacions.Specifications
{
    public class NotificacionByIdentificacionSuscriptorPadreSpec : Specification<BitacoraNotificacion>
    {
        public NotificacionByIdentificacionSuscriptorPadreSpec(DateTime FechaDesde, DateTime FechaHasta, Guid UidSuscriptorPadre, Guid UidNotificPorAprobar, string estadoLeido)
        {
            Query.Where(p => p.FechaCreacion.Date >= FechaDesde.Date && p.FechaCreacion.Date <= FechaHasta.Date && p.Estado == "A" && p.EventoDifusion.Estado == "A")
          .Include(p => p.Cliente).Where(p => p.Cliente.ClientePadreId == UidSuscriptorPadre && p.EventoDifusion.ClasificacionId == UidNotificPorAprobar)
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
