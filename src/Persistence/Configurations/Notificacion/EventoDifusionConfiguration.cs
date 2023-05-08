using EnrolApp.Domain.Entities.Notificacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Notificacion;

public class EventoDifusionConfiguration : IEntityTypeConfiguration<EventoDifusion>
{
    public void Configure(EntityTypeBuilder<EventoDifusion> builder)
    {

        builder.HasKey(x => x.Id);

        builder.HasMany(g => g.ListaBitacoraNotif)
           .WithOne(g => g.EventoDifusion)
           .HasForeignKey(g => g.EventoDifusionId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Codigo)
        .HasMaxLength(20)
        .IsRequired();

        builder.Property(x => x.Descripcion)
       .HasMaxLength(150)
       .IsRequired();

        builder.Property(x => x.UriImage)
       .HasMaxLength(255);

        builder.Property(p => p.Estado)
            .HasDefaultValue("A")
            .ValueGeneratedOnAdd()
            .IsRequired();

    }
}
