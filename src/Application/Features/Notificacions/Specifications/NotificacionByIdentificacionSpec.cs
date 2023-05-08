using Ardalis.Specification;
using EnrolApp.Domain.Entities.Notificacion;

namespace EnrolApp.Application.Features.Notifications.Specifications;

public class NotificacionByIdentificacionSpec : Specification<BitacoraNotificacion>
{
    public NotificacionByIdentificacionSpec(string Identificacion, DateTime FechaDesde, DateTime FechaHasta, string estadoLeido)
    {
        if (string.IsNullOrEmpty(estadoLeido))
        {
            if (string.IsNullOrEmpty(Identificacion))
            {
                Query.Where(p => p.FechaCreacion.Date >= FechaDesde.Date && p.FechaCreacion.Date <= FechaHasta.Date && p.Estado == "A" && p.EventoDifusion.Estado == "A" && p.EstadoLeido == estadoLeido)
                  //.Include(p => p.Cliente).Where(p => p.Cliente.Identificacion == Identificacion)
                  .Include(ev => ev.EventoDifusion)
                  .ThenInclude(evd => evd.Evento)
                  .ThenInclude(evd => evd.Feature)
                  .Include(ev => ev.EventoDifusion)
                  .ThenInclude(evd => evd.Plantilla)
                  .Include(ev => ev.EventoDifusion)
                  .ThenInclude(evd => evd.Clasificacion)
                  .OrderByDescending(x => x.EventoDifusion.Clasificacion.Orden)
                  .ThenByDescending(x => x.FechaCreacion);
            }
            else
            {
                Query.Where(p => p.FechaCreacion.Date >= FechaDesde.Date && p.FechaCreacion.Date <= FechaHasta.Date && p.Estado == "A" && p.EventoDifusion.Estado == "A" && p.EstadoLeido == estadoLeido)
                  .Include(p => p.Cliente).Where(p => p.Cliente.Identificacion == Identificacion)
                  .Include(ev => ev.EventoDifusion)
                  .ThenInclude(evd => evd.Evento)
                  //.ThenInclude(evd => evd.Feature)
                  .Include(ev => ev.EventoDifusion)
                  .ThenInclude(evd => evd.Plantilla)
                  .Include(ev => ev.EventoDifusion)
                  .ThenInclude(evd => evd.Clasificacion)
                  .OrderByDescending(x => x.EventoDifusion.Clasificacion.Orden)
                  .ThenByDescending(x => x.FechaCreacion);
            }
        }
        else
        {
            if (string.IsNullOrEmpty(Identificacion))
            {
                Query.Where(p => p.FechaCreacion.Date >= FechaDesde.Date && p.FechaCreacion.Date <= FechaHasta.Date && p.Estado == "A" && p.EventoDifusion.Estado == "A" && p.EstadoLeido == estadoLeido)
                  .Include(ev => ev.EventoDifusion)
                  .ThenInclude(evd => evd.Evento)
                  .ThenInclude(evd => evd.Feature)
                  .Include(ev => ev.EventoDifusion)
                  .ThenInclude(evd => evd.Plantilla)
                  .Include(ev => ev.EventoDifusion)
                  .ThenInclude(evd => evd.Clasificacion)
                  .OrderByDescending(x => x.EventoDifusion.Clasificacion.Orden)
                  .ThenByDescending(x => x.FechaCreacion);
            }
            else
            {
                Query.Where(p => p.FechaCreacion.Date >= FechaDesde.Date && p.FechaCreacion.Date <= FechaHasta.Date && p.Estado == "A" && p.EventoDifusion.Estado == "A" && p.EstadoLeido == estadoLeido)
                  .Include(p => p.Cliente).Where(p => p.Cliente.Identificacion == Identificacion)
                  .Include(ev => ev.EventoDifusion)
                  .ThenInclude(evd => evd.Evento)
                  //.ThenInclude(evd => evd.Feature)
                  .Include(ev => ev.EventoDifusion)
                  .ThenInclude(evd => evd.Plantilla)
                  .Include(ev => ev.EventoDifusion)
                  .ThenInclude(evd => evd.Clasificacion)
                  .OrderByDescending(x => x.EventoDifusion.Clasificacion.Orden)
                  .ThenByDescending(x => x.FechaCreacion);
            }
        }


    }
}
