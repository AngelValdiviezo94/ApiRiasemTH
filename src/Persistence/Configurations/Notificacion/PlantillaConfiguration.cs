using EnrolApp.Domain.Entities.Notificacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Notificacion;

public class PlantillaConfiguration : IEntityTypeConfiguration<Plantilla>
{
    public void Configure(EntityTypeBuilder<Plantilla> builder)
    {

        builder.HasKey(x => x.Id);

        builder.HasMany(g => g.EventosDifusion)
        .WithOne(g => g.Plantilla)
        .HasForeignKey(g => g.PlantillaId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Codigo)
        .HasMaxLength(20).IsRequired();

        builder.Property(x => x.Mensaje)
       .HasMaxLength(800)
       .IsRequired();

        builder.Property(x => x.MensajeHtml)
        .HasMaxLength(800);

        builder.Property(x => x.Resumen)
        .HasMaxLength(500);

        builder.Property(p => p.Relevante)
             .HasDefaultValue(false)
             .ValueGeneratedOnAdd()
             .IsRequired();

        builder.Property(p => p.RequiereAccion)
            .HasDefaultValue(false)
            .ValueGeneratedOnAdd()
            .IsRequired();


        builder.Property(p => p.RequiereNivelDetalle)
           .HasDefaultValue(false)
           .ValueGeneratedOnAdd()
           .IsRequired();

        builder.Property(p => p.RequiereEvalVariables)
          .HasDefaultValue(false)
          .ValueGeneratedOnAdd()
          .IsRequired();

        builder.Property(p => p.UriImage);

        builder.Property(p => p.Estado)
            .HasDefaultValue("A")
            .ValueGeneratedOnAdd()
            .IsRequired();

    }
}
