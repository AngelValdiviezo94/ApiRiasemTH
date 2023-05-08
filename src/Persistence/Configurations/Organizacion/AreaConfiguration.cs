using EnrolApp.Domain.Entities.Organizacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnrolApp.Persistence.Configurations.Organizacion;

public class AeraConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {

        builder.HasKey(x => x.Id);
        builder.HasMany(g => g.Departamentos)
            .WithOne(g => g.Area)
            .HasForeignKey(g => g.AreaId)
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
