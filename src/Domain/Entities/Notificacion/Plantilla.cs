
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnrolApp.Domain.Entities.Notificacion;

[Table("NT_Plantilla", Schema = "dbo")]
public class Plantilla
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("codigo", Order = 1, TypeName = "varchar")]
    [StringLength(50)]
    public string Codigo { get; set; }

    [Column("mensaje", Order = 2, TypeName = "varchar")]
    [StringLength(800)]
    public string Mensaje { get; set; }

    [Column("resumen", Order = 3, TypeName = "varchar")]
    [StringLength(500)]
    public string Resumen { get; set; }

    [Column("relevante", Order = 4, TypeName = "bit")]
    [StringLength(1)]
    public bool Relevante { get; set; }

    [Column("requiereAccion", Order = 5, TypeName = "bit")]
    public bool RequiereAccion { get; set; }

    [Column("uriImage", Order = 6, TypeName = "varchar")]
    [StringLength(255)]
    public string UriImage { get; set; }

    [Column("estado", Order = 7, TypeName = "varchar")]
    [MaxLength(1)]
    public string Estado { get; set; }

    [Column("MensajeHtml", Order = 8, TypeName = "varchar")]
    [StringLength(800)]
    public string MensajeHtml { get; set; }

    [Column("requiereNivelDetalle", Order = 9, TypeName = "bit")]
    public bool RequiereNivelDetalle { get; set; }

    [Column("requiereEvalVariables", Order = 10, TypeName = "bit")]
    public bool RequiereEvalVariables { get; set; }

    public virtual ICollection<EventoDifusion> EventosDifusion { get; set; }
}
