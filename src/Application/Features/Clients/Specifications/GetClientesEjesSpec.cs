using Ardalis.Specification;
using EnrolApp.Domain.Entities.Organizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Clients.Specifications;

public class GetClientesEjesSpec : Specification<CargoEje>
{
    public GetClientesEjesSpec(string CodUdn, string CodArea, string Scc, string Identificacion)
    {
        Query.Where(p => p.Estado == "A" && Identificacion == "" ? p.Identificacion == p.Identificacion : (p.Identificacion == Identificacion))
             .Where(p => p.CargoSG.Departamento.CodigoHomologacion == (Scc == "" ? p.CargoSG.Departamento.CodigoHomologacion : Scc))
             .Where(p => p.CargoSG.Departamento.Area.CodigoHomologacion == (CodArea == "" ? p.CargoSG.Departamento.Area.CodigoHomologacion : CodArea))
             .Where(p => p.CargoSG.Departamento.Area.Empresa.CodigoHomologacion == (CodUdn == "" ? p.CargoSG.Departamento.Area.Empresa.CodigoHomologacion : CodUdn))
             .Include(p => p.CargoSG)
             .ThenInclude(p => p.Departamento)
             .ThenInclude(p => p.Area)
             .ThenInclude(p => p.Empresa);
    }
}