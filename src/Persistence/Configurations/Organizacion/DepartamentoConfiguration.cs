using EnrolApp.Domain.Entities.Organizacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Organizacion;

public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> builder)
    {

        builder.HasKey(x => x.Id);
        builder.HasMany(g => g.Prospectos)
            .WithOne(g => g.Departamento)
            .HasForeignKey(g => g.DepartamentoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(g => g.Cargos)
        //    .WithOne(g => g.Departamento)
        //    .HasForeignKey(g => g.DepartamentoId)
        //    .IsRequired()
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(g => g.Clientes)
        //   .WithOne(g => g.Departamento)
        //   .HasForeignKey(g => g.DepartamentoId)
        //   .IsRequired()
        //   .OnDelete(DeleteBehavior.Cascade);

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
