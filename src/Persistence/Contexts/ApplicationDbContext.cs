

using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Familiares;
using EnrolApp.Domain.Entities.Horario;
using EnrolApp.Domain.Entities.Justificacion;
using EnrolApp.Domain.Entities.Marketing;
using EnrolApp.Domain.Entities.Notificacion;
using EnrolApp.Domain.Entities.Organizacion;
using EnrolApp.Domain.Entities.Permisos;
using EnrolApp.Domain.Entities.Seguridad;
using EnrolApp.Domain.Entities.Suscripcion;
using EnrolApp.Domain.Entities.Vacaciones;
using Microsoft.EntityFrameworkCore;
namespace EnrolApp.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    //public DbSet<Cargo> Cargo => Set<Cargo>();
    public DbSet<Prospecto> Prospectos => Set<Prospecto>();
    public DbSet<TipoRelacion> TipoRelacion => Set<TipoRelacion>();

    public DbSet<TipoSuscriptor> TipoSuscriptor => Set<TipoSuscriptor>();
    public DbSet<Departamento> Departamento => Set<Departamento>();
    public DbSet<Area> Area => Set<Area>();
    public DbSet<Empresa> Empresa => Set<Empresa>();
    public DbSet<GrupoEmpresarial> GrupoEmpresarial => Set<GrupoEmpresarial>();
    public DbSet<NotificacionMotivo> NotificacionMotivo => Set<NotificacionMotivo>();

    public DbSet<SolicitudJustificacion> SolicitudJustificacion => Set<SolicitudJustificacion>();

    public DbSet<TipoJustificacion> TipoJustificacion => Set<TipoJustificacion>();

    public DbSet<SolicitudPermiso> SolicitudPermiso => Set<SolicitudPermiso>();

    public DbSet<TipoPermiso> TipoPermiso => Set<TipoPermiso>();


    public DbSet<SolicitudVacacion> SolicitudVacacion => Set<SolicitudVacacion>();
    public DbSet<TurnoEnrol> TurnoEnrols => Set<TurnoEnrol>();
    public DbSet<LocalidadColaborador> LocalidadColaborador => Set<LocalidadColaborador>();
    public DbSet<Localidad> Localidad => Set<Localidad>();

    public DbSet<ColaboradorConvivencia> ColaboradorConvivencia => Set<ColaboradorConvivencia>();
    public DbSet<RolCargoSG> RolCargoSG => Set<RolCargoSG>();
    public DbSet<RolContenidoMK> RolContenidoMK => Set<RolContenidoMK>();
    public DbSet<ContenidoCategoriaMK> ContenidoCategoriaMK => Set<ContenidoCategoriaMK>();
    public DbSet<CargoEje> cargoEjes => Set<CargoEje>();
    public DbSet<TipoRelacionFamiliar> TipoRelacionFamiliar => Set<TipoRelacionFamiliar>();
    public DbSet<FamiliarColaborador> FamiliarColaborador => Set<FamiliarColaborador>();
    public DbSet<SolicitudReemplazoColaborador> SolicitudReemplazoColaborador => Set<SolicitudReemplazoColaborador>();
    public DbSet<Parametros> Parametros => Set<Parametros>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        //modelBuilder.HasDefaultSchema("MyCustomSchema");
        //modelBuilder.Ignore<Cliente>();//por pruebas
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }


    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
    //    {
    //        switch (entry.State)
    //        {
    //            case EntityState.Added:
    //                entry.Entity.Created = _dateTimeService.NowUtc;
    //                entry.Entity.Uid = new Guid();
    //                break;
    //            case EntityState.Modified:
    //                entry.Entity.LastModified = _dateTimeService.NowUtc;
    //                break;
    //        }
    //    }
    //    return await base.SaveChangesAsync(cancellationToken);
    //}
}