using Ardalis.Specification;
using EnrolApp.Domain.Entities.Justificacion;
using EnrolApp.Domain.Entities.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Notificacions.Specifications;

public class SolicitudPermisoByIdSpec : Specification<SolicitudPermiso>
{
    public SolicitudPermisoByIdSpec(Guid? idSolicitud)
    {
        Query.Where(x => x.Id == idSolicitud)
            .Include(x => x.TipoPermiso);
    }

}
