using EnrolApp.Domain.Entities.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Persistence.Configurations.Seguridad;

public class CargoSGConfiguration : IEntityTypeConfiguration<CargoSG>
{
    public void Configure(EntityTypeBuilder<CargoSG> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(g => g.CargoEje)
            .WithOne(g => g.CargoSG)
            .HasForeignKey(g => g.IdCargo)
            .IsRequired();
    }
}