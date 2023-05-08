using EnrolApp.Domain.Entities.Organizacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Organizacion;

public class GrupoEmpresarialConfiguration : IEntityTypeConfiguration<GrupoEmpresarial>
{
    public void Configure(EntityTypeBuilder<GrupoEmpresarial> builder)
    {

        builder.HasKey(x => x.Id);
        builder.HasMany(g => g.Empresas)
            .WithOne(g => g.GrupoEmpresarial)
            .HasForeignKey(g => g.GrupoEmpresarialId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Codigo)
            .HasMaxLength(15);

        builder.Property(x => x.Nombre)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Estado)
            .HasMaxLength(1)
            .IsRequired();
    }
}
