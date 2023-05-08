using EnrolApp.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Notificacion;

[Table("NT_NotificacionMotivo", Schema = "dbo")]
public class NotificacionMotivo
{
    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("tipoFeatureId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid TipoFeatureId { get; set; }
    public virtual Feature Feature { get; set; }

    [Column("tipoMotivoId", Order = 2, TypeName = "uniqueidentifier")]
    public Guid TipoMotivoId { get; set; }
    

    [Column("aprobadorId", Order = 3, TypeName = "uniqueidentifier")]
    public Guid AprobadorId { get; set; }
    public virtual Cliente Cliente { get; set; } 

    [Column("fechaCreacion", Order = 4)]
    public DateTime FechaCreacion { get; set; }

    [Column("usuarioCreacion", Order = 5)]
    public string UsuarioCreacion { get; set; }

    [Column("fechaModificacion", Order = 6)]
    public DateTime? FechaModificacion { get; set; }

    [Column("usuarioModificacion", Order = 7)]
    public string UsuarioModificacion { get; set; }

    [Column("estado", Order = 8)]
    [StringLength(1)]
    public string Estado { get; set; }

}
