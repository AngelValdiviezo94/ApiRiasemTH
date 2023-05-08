using Ardalis.Specification;
using EnrolApp.Domain.Entities.Justificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Notificacions.Specifications;

public class SolicitudJustificacionByIdSpec : Specification<SolicitudJustificacion>
{
    public SolicitudJustificacionByIdSpec(Guid? idSolicitud)
    {
        Query.Where(x => x.Id == idSolicitud)
            .Include(x => x.TipoJustificacion);
    }

}
