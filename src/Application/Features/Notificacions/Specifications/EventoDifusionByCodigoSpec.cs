using Ardalis.Specification;
using EnrolApp.Domain.Entities.Notificacion;

namespace EnrolApp.Application.Features.Notifications.Specifications;

public class EventoDifusionByCodigoSpec : Specification<EventoDifusion>
{
    public EventoDifusionByCodigoSpec(string CodigoEvento)
    {

        Query.Include(ev => ev.Evento).Where(ev => ev.Evento.Codigo.ToUpper().Trim() == CodigoEvento.ToUpper().Trim())
          .Include(ev => ev.Plantilla).AsNoTracking();
    }
}
