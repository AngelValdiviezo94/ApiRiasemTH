using EnrolApp.Domain.Entities.Permisos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Persistence.Configurations.Permiso;

public class TipoPermisoConfiguration : IEntityTypeConfiguration<TipoPermiso>
{
    public void Configure(EntityTypeBuilder<TipoPermiso> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(g => g.SolicitudPermiso)
            .WithOne(g => g.TipoPermiso)
            .HasForeignKey(g => g.IdTipoPermiso)
            .IsRequired();
        builder.Property(x => x.Codigo)
            .HasMaxLength(5)
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
