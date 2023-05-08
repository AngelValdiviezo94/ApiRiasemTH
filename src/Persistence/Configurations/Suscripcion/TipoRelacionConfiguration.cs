using EnrolApp.Domain.Entities.Suscripcion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Suscripcion;

public class TipoRelacionConfiguration : IEntityTypeConfiguration<TipoRelacion>
{
    public void Configure(EntityTypeBuilder<TipoRelacion> builder)
    {

        builder.HasKey(x => x.Id);
        builder.HasMany(g => g.Prospectos)
            .WithOne(g => g.TipoRelacion)
            .HasForeignKey(g => g.TipoRelacionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(g => g.Clientes)
        //   .WithOne(g => g.TipoRelacion)
        //   .HasForeignKey(g => g.TipoRelacionId)
        //   .IsRequired()
        //   .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Codigo)
        .HasMaxLength(15)
        .IsRequired();

        builder.Property(x => x.Descripcion)
       .HasMaxLength(50)
       .IsRequired();

        builder.Property(x => x.Estado)
      .HasMaxLength(1)
      .IsRequired();
    }
}
