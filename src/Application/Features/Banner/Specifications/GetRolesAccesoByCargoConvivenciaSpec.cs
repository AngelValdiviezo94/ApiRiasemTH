using Ardalis.Specification;
using EnrolApp.Domain.Entities.Seguridad;

namespace EnrolApp.Application.Features.Banner.Specifications
{
    public class GetRolesAccesoByCargoConvivenciaSpec : Specification<RolCargoSG>
    {
        public GetRolesAccesoByCargoConvivenciaSpec(string codCargoConvivencia, string codScc, Guid uidCanal, string tipoCliente)
        {
            if (tipoCliente == "EJE" || tipoCliente == "FAMILIAR")
            {
                Query.Where(rca => rca.CargoSG.Id == Guid.Parse(codCargoConvivencia) &&
                                   //rca.CargoSG.Departamento.Id == Guid.Parse(codScc) &&
                                   rca.RolSG.CanalSGId == uidCanal &&
                                   rca.Estado == "A");
            }
            else
            {
                Query.Where(rca => rca.CargoSG.CodigoHomologacion == codCargoConvivencia &&
                                   rca.CargoSG.Departamento.CodigoHomologacion == codScc &&
                                   rca.RolSG.CanalSGId == uidCanal &&
                                   rca.Estado == "A");
            }
        }
    }
}
