
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnrolApp.Domain.Entities.Notificacion;

[Table("NT_Evento", Schema = "dbo")]
public class Evento
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("featureId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid FeatureId { get; set; }
    public virtual Feature Feature { get; set; }

    [Column("codigo", Order = 2, TypeName = "varchar")]
    [StringLength(20)]
    public string Codigo { get; set; }

    [Column("descripcion", Order = 3, TypeName = "varchar")]
    [StringLength(150)]
    public string Descripcion { get; set; }

    [Column("estado", Order = 4, TypeName = "varchar")]
    [MaxLength(1)]
    public string Estado { get; set; }

    public virtual ICollection<EventoDifusion> EventosDifusion { get; set; }
}

