using EnrolApp.Domain.Entities.Notificacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Notificacion;

public class BitacoraConfiguration : IEntityTypeConfiguration<BitacoraNotificacion>
{
    public void Configure(EntityTypeBuilder<BitacoraNotificacion> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Identificacion).HasMaxLength(20).IsRequired();
        builder.Property(x => x.SolicitudId);
        builder.Property(x => x.TipoSolicitud)
        .HasMaxLength(30);

        builder.Property(x => x.ReferenciaClienteId);

        builder.Property(x => x.Mensaje)
       .HasMaxLength(800)
       .IsRequired();

        builder.Property(x => x.MensajeHtml)
       .HasMaxLength(800);

        builder.Property(x => x.Resumen)
       .HasMaxLength(500);

        builder.Property(p => p.FechaCreacion)
             .HasColumnType("timestamp without time zone")
             .HasDefaultValueSql("NOW()")
             .ValueGeneratedOnAdd()
             .IsRequired();

        builder.Property(x => x.FechaActualizacion);

        builder.Property(x => x.EstadoLeido)
       .HasMaxLength(1)
       .HasDefaultValue("N")
       .ValueGeneratedOnAdd()
       .IsRequired();

        builder.Property(p => p.Estado)
            .HasDefaultValue("A")
            .ValueGeneratedOnAdd()
            .IsRequired();

    }
}
