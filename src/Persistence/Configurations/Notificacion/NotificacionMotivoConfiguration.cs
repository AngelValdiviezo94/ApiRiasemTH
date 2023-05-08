using EnrolApp.Domain.Entities.Notificacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Persistence.Configurations.Notificacion;

public class NotificacionMotivoConfiguration : IEntityTypeConfiguration<NotificacionMotivo>
{
    public void Configure(EntityTypeBuilder<NotificacionMotivo> builder)
    {
        builder.HasKey(x => x.Id);

    }
}
