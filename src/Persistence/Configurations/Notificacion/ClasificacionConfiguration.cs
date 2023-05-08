using EnrolApp.Domain.Entities.Notificacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Notificacion;

public class ClasificacionConfiguration : IEntityTypeConfiguration<Clasificacion>
{
    public void Configure(EntityTypeBuilder<Clasificacion> builder)
    {

        builder.HasKey(x => x.Id);

        builder.HasMany(g => g.EventosDifusion)
          .WithOne(g => g.Clasificacion)
          .HasForeignKey(g => g.ClasificacionId)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Codigo)
        .HasMaxLength(20).IsRequired();

        builder.Property(x => x.Descripcion)
       .HasMaxLength(150)
       .IsRequired();

        builder.Property(x => x.UriImage)
        .HasMaxLength(255);


        builder.Property(x => x.Orden)
        .IsRequired();

        builder.Property(p => p.Estado)
            .HasDefaultValue("A")
            .ValueGeneratedOnAdd()
            .IsRequired();

    }
}
