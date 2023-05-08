using Ardalis.Specification;
using EnrolApp.Domain.Entities.Notificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Notificacions.Specifications;

public class ListTipoMotivoSpec : Specification<NotificacionMotivo>
{
    public ListTipoMotivoSpec()
    {
        Query.Where(x => x.Estado == "A")
            .Include(x => x.Feature).Where(x => x.Feature.Codigo == "JUS" || x.Feature.Codigo == "PER");
    }
}
