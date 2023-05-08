using Ardalis.Specification;
using EnrolApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Prospectos.Specifications;

public class ClienteByIdentificacionRelacionadoSpec : Specification<Cliente>
{
    public ClienteByIdentificacionRelacionadoSpec(string TipoIdentificacion, string Identificacion)
    {
        Query.Where(p => p.TipoIdentificacion == TipoIdentificacion && p.Identificacion == Identificacion);
            //.Include(p => p.Cargo)
            //.ThenInclude(p => p.Departamento)
            //.ThenInclude(p => p.Area)
            //.ThenInclude(p => p.Empresa)
            //.ThenInclude(p => p.GrupoEmpresarial);

    }
}