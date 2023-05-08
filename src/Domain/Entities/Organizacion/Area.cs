
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnrolApp.Domain.Entities.Organizacion;

[Table("OG_Area", Schema = "dbo")]
public class Area
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("empresaId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid EmpresaId { get; set; }
    public virtual Empresa Empresa { get; set; }

    [Column("codigo", Order = 2, TypeName = "varchar")]
    [StringLength(15)]
    public string Codigo { get; set; }

    [Column("nombre", Order = 3, TypeName = "varchar")]
    [StringLength(50)]
    public string Nombre { get; set; }

    [Column("codigoHomologacion", Order = 4, TypeName = "varchar")]
    public string CodigoHomologacion { get; set; }

    [Column("nombreHomologacion", Order = 5, TypeName = "varchar")]
    public string NombreHomologacion { get; set; }

    [Column("estado", Order = 6, TypeName = "varchar")]
    [StringLength(1)]
    public string Estado { get; set; }

    public virtual ICollection<Departamento> Departamentos { get; set; }
}
