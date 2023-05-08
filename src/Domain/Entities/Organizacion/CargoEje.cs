using EnrolApp.Domain.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Domain.Entities.Organizacion;

[Table("SG_CargoEjes", Schema = "dbo")]
public class CargoEje
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("identificacion", Order = 1, TypeName = "varchar")]
    public string Identificacion { get; set; }

    [Column("idUdn", Order = 2, TypeName = "uniqueidentifier")]
    public Guid IdUdn { get; set; }

    public virtual Empresa Empresa { get; set; }

    [Column("idCargo", Order = 3, TypeName = "uniqueidentifier")]
    public Guid IdCargo { get; set; }

    public virtual CargoSG CargoSG { get; set; }

    [Column("estado", Order = 4, TypeName = "varchar")]
    public string Estado { get; set; }

    [Column("usuarioCreacion", Order = 5, TypeName = "varchar")]
    public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 6, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 7, TypeName = "varchar")]
    public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 8, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }
}
