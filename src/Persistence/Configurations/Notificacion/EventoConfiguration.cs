using EnrolApp.Domain.Entities.Notificacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Notificacion;

public class EventoConfiguration : IEntityTypeConfiguration<Evento>
{
    public void Configure(EntityTypeBuilder<Evento> builder)
    {

        builder.HasKey(x => x.Id);

        builder.HasMany(g => g.EventosDifusion)
           .WithOne(g => g.Evento)
           .HasForeignKey(g => g.EventoId)
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
