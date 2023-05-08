using EnrolApp.Domain.Entities.Horario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Persistence.Configurations.Horario;

//public class CheckInOutConfiguration : IEntityTypeConfiguration<CheckInOut>
//{
//    public void Configure(EntityTypeBuilder<CheckInOut> builder)
//    {
//        builder.HasKey(x => new { x.UserId, x.CheckTime });
//        builder.Property(x => x.UserId).ValueGeneratedOnAdd();

//        builder.Property(x => x.CheckTime)
//            .ValueGeneratedOnAddOrUpdate()
//            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
//    }
//}
