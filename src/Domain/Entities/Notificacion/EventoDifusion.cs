
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnrolApp.Domain.Entities.Notificacion;

[Table("NT_EventoDifusion", Schema = "dbo")]
public class EventoDifusion
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("eventoId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid EventoId { get; set; }
    public virtual Evento Evento { get; set; }

    [Column("clasificacionId", Order = 2, TypeName = "uniqueidentifier")]
    public Guid ClasificacionId { get; set; }
    public virtual Clasificacion Clasificacion { get; set; }

    [Column("plantillaId", Order = 3, TypeName = "uniqueidentifier")]
    public Guid PlantillaId { get; set; }
    public virtual Plantilla Plantilla { get; set; }

    [Column("codigo", Order = 4, TypeName = "varchar")]
    [StringLength(20)]
    public string Codigo { get; set; }

    [Column("descripcion", Order = 5, TypeName = "varchar")]
    [StringLength(150)]
    public string Descripcion { get; set; }

    [Column("uriImage", Order = 6, TypeName = "varchar")]
    [StringLength(255)]
    public string UriImage { get; set; }

    [Column("estado", Order = 7, TypeName = "varchar")]
    [MaxLength(1)]
    public string Estado { get; set; }

    public virtual ICollection<BitacoraNotificacion> ListaBitacoraNotif { get; set; }
}

