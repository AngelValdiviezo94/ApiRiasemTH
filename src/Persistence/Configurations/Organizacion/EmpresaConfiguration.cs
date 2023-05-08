using EnrolApp.Domain.Entities.Organizacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Organizacion;

public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {

        builder.HasKey(x => x.Id);
        builder.HasMany(g => g.Areas)
            .WithOne(g => g.Empresa)
            .HasForeignKey(g => g.EmpresaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(g => g.CargoEje)
            .WithOne(g => g.Empresa)
            .HasForeignKey(g => g.IdUdn)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Codigo)
            .HasMaxLength(15);

        builder.Property(x => x.NombreComercial)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.RazonSocial)
          .HasMaxLength(80)
          .IsRequired();

        builder.Property(x => x.Ruc)
         .HasMaxLength(13)
         .IsRequired();

        builder.Property(x => x.Estado)
            .HasMaxLength(1)
            .IsRequired();
    }
}
