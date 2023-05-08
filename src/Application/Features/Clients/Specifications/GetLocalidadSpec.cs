using Ardalis.Specification;
using EnrolApp.Domain.Entities.Horario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Clients.Specifications
{
    public class GetLocalidadSpec : Specification<Localidad>
    {
        public GetLocalidadSpec()
        {
            Query.Where(p => p.Estado == "A");
        }
    }
}
