
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnrolApp.Domain.Entities.Notificacion;

[Table("NT_Clasificacion", Schema = "dbo")]
public class Clasificacion
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("codigo", Order = 1, TypeName = "varchar")]
    [StringLength(20)]
    public string Codigo { get; set; }

    [Column("descripcion", Order = 2, TypeName = "varchar")]
    [StringLength(150)]
    public string Descripcion { get; set; }

    [Column("uriImage", Order = 3, TypeName = "varchar")]
    [StringLength(255)]
    public string UriImage { get; set; }

    [Column("orden", Order = 4, TypeName = "int")]
    public int Orden { get; set; }

    [Column("estado", Order = 5, TypeName = "varchar")]
    [MaxLength(1)]
    public string Estado { get; set; }

    public virtual ICollection<EventoDifusion> EventosDifusion { get; set; }
}

