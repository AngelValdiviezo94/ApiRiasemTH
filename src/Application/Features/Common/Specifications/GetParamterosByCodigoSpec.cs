using Ardalis.Specification;
using EnrolApp.Domain.Entities.Common;

namespace EnrolApp.Application.Features.Common.Specifications
{
    public class GetParamterosByCodigoSpec : Specification<Parametros>
    {
        public GetParamterosByCodigoSpec(string codigo)
        {
            Query.Where(p => p.Codigo == codigo && p.Estado == "A");
        }
    }
}
