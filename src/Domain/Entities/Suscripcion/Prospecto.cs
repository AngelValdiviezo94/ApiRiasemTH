using EnrolApp.Domain.Entities.Organizacion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnrolApp.Domain.Entities.Suscripcion;

[Table("CL_Prospecto", Schema = "dbo")]
public class Prospecto
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("tipoRelacionId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid TipoRelacionId { get; set; }
    public virtual TipoRelacion TipoRelacion { get; set; }

    [Column("tipoSuscriptorId", Order = 2, TypeName = "uniqueidentifier")]
    public Guid TipoSuscriptorId { get; set; }
    public virtual TipoSuscriptor TipoSuscriptor { get; set; }

    [Column("departamentoId", Order = 3, TypeName = "uniqueidentifier")]
    public Guid DepartamentoId { get; set; }
    public virtual Departamento Departamento { get; set; }
    
    //public virtual Cliente Cliente { get; set; }

    [Column("tipoIdentificacion", Order = 4, TypeName = "varchar")]
    [StringLength(1)]
    public string TipoIdentificacion { get; set; }

    [Column("identificacion", Order = 5, TypeName = "varchar")]
    [StringLength(20)]
    public string Identificacion { get; set; }

    [Column("nombres", Order = 6, TypeName = "varchar")]
    [StringLength(150)]
    public string Nombres { get; set; }

    [Column("apellidos", Order = 7, TypeName = "varchar")]
    [StringLength(150)]
    public string Apellidos { get; set; }

    [Column("alias", Order = 8, TypeName = "varchar")]
    [StringLength(50)]
    public string Alias { get; set; }

    [Column("celular", Order = 9, TypeName = "varchar")]
    [StringLength(10)]
    public string Celular { get; set; }

    [Column("tipoIdentificacionFamiliar", Order = 10, TypeName = "varchar")]
    [StringLength(1)]
    public string TipoIdentificacionFamiliar { get; set; }

    [Column("indentificacionFamiliar", Order = 11, TypeName = "varchar")]
    [StringLength(20)]
    public string IndentificacionFamiliar { get; set; }

    [Column("email", Order = 12, TypeName = "varchar")]
    [StringLength(80)]
    public string Email { get; set; }

    [Column("estado", Order = 13, TypeName = "varchar")]
    [StringLength(1)]
    public string Estado { get; set; }

    [NotMapped]
    public string GrupoEmpresarial { get; set; }

    [NotMapped]
    public string FechaNacimiento { get; set; }

    [NotMapped]
    public string Genero { get; set; }

    [NotMapped]
    public string Direccion { get; set; }

    [NotMapped]
    public float Latitud { get; set; }

    [NotMapped]
    public float Longitud { get; set; }   
}
