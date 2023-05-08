using EnrolApp.Domain.Entities.Suscripcion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Suscripcion;

public class TipoSuscriptorConfiguration : IEntityTypeConfiguration<TipoSuscriptor>
{
    public void Configure(EntityTypeBuilder<TipoSuscriptor> builder)
    {

        builder.HasKey(x => x.Id);
        builder.HasMany(g => g.Prospectos)
            .WithOne(g => g.TipoSuscriptor)
            .HasForeignKey(g => g.TipoSuscriptorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(g => g.Clientes)
        //   .WithOne(g => g.TipoSuscriptor)
        //   .HasForeignKey(g => g.TipoSuscriptorId)
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
