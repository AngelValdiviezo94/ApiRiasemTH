using Ardalis.Specification;
using EnrolApp.Domain.Entities.Notificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Notificacions.Specifications;

public class NotificacionByTipoMotivoSpec : Specification<BitacoraNotificacion>
{
    public NotificacionByTipoMotivoSpec(DateTime FechaDesde, DateTime FechaHasta, Guid clienteId, Guid UidNotificPorAprobar)
    {
        Query.Where(p => p.FechaCreacion.Date >= FechaDesde.Date && p.FechaCreacion.Date <= FechaHasta.Date 
                    && p.Estado == "A" && p.EventoDifusion.Estado == "A")
          .Include(p => p.Cliente).Where(p => p.ClienteId == clienteId)
          .Include(ev => ev.EventoDifusion)
          .ThenInclude(evd => evd.Evento)
          .ThenInclude(f => f.Feature)
          .Include(ev => ev.EventoDifusion)
          .ThenInclude(evd => evd.Plantilla)
          .Include(ev => ev.EventoDifusion)
          .ThenInclude(evd => evd.Clasificacion)
          
          .OrderByDescending(x => x.EventoDifusion.Clasificacion.Orden)
          .ThenByDescending(x => x.FechaCreacion);
    }
}