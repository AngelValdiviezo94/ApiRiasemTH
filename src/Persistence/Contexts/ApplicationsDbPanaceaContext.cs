using EnrolApp.Domain.Entities.Nomina;
using Microsoft.EntityFrameworkCore;

namespace EnrolApp.Persistence.Contexts;

public class ApplicationsDbPanaceaContext : DbContext
{

    public ApplicationsDbPanaceaContext(DbContextOptions<ApplicationsDbPanaceaContext> options)
            : base(options)
    {
        //options.LazyLoadingEnabled = false;
    }

    //public DbSet<Empleado> Empleados => Set<Empleado>();
    //public DbSet<CertificadoLaboral> CertificadoLaboral => Set<CertificadoLaboral>();
    //public DbSet<InformacionGeneralEmpleado> InfoGeneralEmpleado => Set<InformacionGeneralEmpleado>();

}
