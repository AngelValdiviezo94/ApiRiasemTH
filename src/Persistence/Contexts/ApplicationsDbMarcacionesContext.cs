using EnrolApp.Domain.Entities.Horario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Persistence.Contexts;

public class ApplicationsDbMarcacionesContext : DbContext
{
    public ApplicationsDbMarcacionesContext(DbContextOptions<ApplicationsDbMarcacionesContext> options) 
        : base(options)
    {
        
        //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<CheckInOut> CheckInOut { get; set; }
    public DbSet<UserInfo> UserInfo { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CheckInOut>().HasKey(x => new { x.UserId, x.CheckTime });
        //modelBuilder.Entity<CheckInOut>().Property(x => x.UserId).D
        //modelBuilder.HasDefaultSchema("MyCustomSchema");
        //modelBuilder.Entity<CheckInOut>().HasNoKey();
        //modelBuilder.Ignore<Cliente>();//por pruebas
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationsDbMarcacionesContext).Assembly);
    }
}



