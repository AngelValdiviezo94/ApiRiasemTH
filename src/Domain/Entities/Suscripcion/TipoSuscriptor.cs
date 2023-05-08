using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnrolApp.Domain.Entities.Suscripcion;

[Table("CL_TipoSuscriptor", Schema = "dbo")]
public class TipoSuscriptor
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("codigo", Order = 1, TypeName = "varchar")]
    [StringLength(15)]
    public string Codigo { get; set; }

    [Column("descripcion", Order = 2, TypeName = "varchar")]
    [StringLength(50)]
    public string Descripcion { get; set; }

    [Column("estado", Order = 3, TypeName = "varchar")]
    [StringLength(1)]
    public string Estado { get; set; }

    public virtual ICollection<Prospecto> Prospectos { get; set; }
    //public virtual ICollection<Cliente> Clientes { get; set; }
}
