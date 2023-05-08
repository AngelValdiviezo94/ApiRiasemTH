using EnrolApp.Domain.Entities.Notificacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Notificacion;

public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {

        builder.HasKey(x => x.Id);

        builder.HasMany(g => g.Eventos)
           .WithOne(g => g.Feature)
           .HasForeignKey(g => g.FeatureId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(g => g.NotificacionMotivo)
           .WithOne(g => g.Feature)
           .HasForeignKey(g => g.TipoFeatureId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Codigo)
        .HasMaxLength(20)
        .IsRequired();

        builder.Property(x => x.Descripcion)
       .HasMaxLength(150)
       .IsRequired();

        builder.Property(p => p.Estado)
            .HasDefaultValue("A")
            .ValueGeneratedOnAdd()
            .IsRequired();

    }
}
