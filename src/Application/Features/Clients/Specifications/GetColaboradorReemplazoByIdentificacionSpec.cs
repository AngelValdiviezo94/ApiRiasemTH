using Ardalis.Specification;
using EnrolApp.Domain.Entities.Notificacion;

namespace EnrolApp.Application.Features.Clients.Specifications
{
    public class GetColaboradorReemplazoByIdentificacionSpec : Specification<SolicitudReemplazoColaborador>
    {
        public GetColaboradorReemplazoByIdentificacionSpec(string Identificacion, DateTime fechaActual, string tipo)
        {
            if (tipo == "1")
            {
                Query.Where(p => p.IdentificacionReemplazo == Identificacion
                        && p.FechaDesde.Date <= fechaActual.Date && p.FechaHasta.Date >= fechaActual.Date
                        && p.Estado == "A");
            }

            if (tipo == "2")
            {
                Query.Where(p => p.IdentificacionColaborador == Identificacion
                        && p.FechaDesde.Date <= fechaActual.Date && p.FechaHasta.Date >= fechaActual.Date
                        && p.Estado == "A");
            }
        }
    }
}
