using Ardalis.Specification;
using EnrolApp.Domain.Entities.Organizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Employees.Specifications;

public class GetCargoEjeSpec : Specification<CargoEje>
{
    public GetCargoEjeSpec(string identificacion)
    {
        Query.Where(p => p.Identificacion == identificacion)
            .Include(x => x.CargoSG)
            .ThenInclude(x => x.Departamento)
            .ThenInclude(x => x.Area)
            .ThenInclude(x => x.Empresa)
            .ThenInclude(x => x.GrupoEmpresarial);
            //.Include(x => x.Empresa);
    }
}
