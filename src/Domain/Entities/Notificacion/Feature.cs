using System.ComponentModel.DataAnnotations.Schema;
namespace EnrolApp.Domain.Entities.Notificacion;

[Table("WF_Feature", Schema = "dbo")]
public class Feature
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("canalId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid CanalId { get; set; }
    //public virtual Canal Canal { get; set; }

    [Column("codigo", Order = 2, TypeName = "varchar")]
    public string Codigo { get; set; }

    [Column("nombre", Order = 3, TypeName = "varchar")]
    public string Nombre { get; set; }

    [Column("descripcion", Order = 4, TypeName = "varchar")]
    public string Descripcion { get; set; }

    [Column("orden", Order = 5, TypeName = "int")]
    public int? Orden { get; set; }

    [Column("estado", Order = 6, TypeName = "varchar")]
    public string Estado { get; set; }

    [Column("usuarioCreacion", Order = 7, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 8, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 9, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 10, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<NotificacionMotivo> NotificacionMotivo { get; set; }
    public virtual ICollection<Evento> Eventos { get; set; }
}

