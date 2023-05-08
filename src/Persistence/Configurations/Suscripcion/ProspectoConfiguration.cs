using EnrolApp.Domain.Entities;
using EnrolApp.Domain.Entities.Suscripcion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Suscripcion;

public class ProspectoConfiguration : IEntityTypeConfiguration<Prospecto>
{

    public void Configure(EntityTypeBuilder<Prospecto> builder)
    {

        builder.HasKey(x => x.Id);

        //builder.HasOne(a => a.Cliente)
        //   .WithOne(b => b.Prospecto)
        //   .HasForeignKey<Cliente>(a => a.ProspectoRef);

        //builder.

        builder.Property(x => x.TipoIdentificacion)
            .HasMaxLength(1)
            .IsRequired();

        builder.Property(x => x.Identificacion)
          .HasMaxLength(20)
          .IsRequired();

        builder.Property(p => p.Nombres)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Apellidos)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Alias)
            .HasMaxLength(50);

        builder.Property(x => x.Celular)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.TipoIdentificacionFamiliar)
            .HasMaxLength(1);

        builder.Property(x => x.IndentificacionFamiliar)
           .HasMaxLength(20);

        builder.Property(x => x.Estado)
          .HasMaxLength(1)
          .IsRequired();

    }
}
