using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Nomina;

public class InformacionGeneralEmpleado
{

    [Key]
    [Column("codigoEmpleado")]
    public int CodigoEmpleado { get; set; }

    [Column("tipoIdentificacion")]

    public string TipoIdentificacion { get; set; }

    [Column("identificacion")]

    public string Identificacion { get; set; }

    [Column("nombres")]

    public string Nombres { get; set; }

    [Column("apellidos")]
    public string Apellidos { get; set; }

    [Column("empresa")]
    public string Empresa { get; set; }

    [Column("cargo")]
    public string Cargo { get; set; }

    [Column("fechaIngreso")]
    public DateTime FechaIngreso { get; set; }

    [Column("cod_empresa")]
    public string Cod_Empresa { get; set; }

    [Column("area")]
    public string Area { get; set; }

    [Column("rucEmpresa")]
    public string RucEmpresa { get; set; }

    [Column("grupoEmpresarial")]
    public string GrupoEmpresarial { get; set; }

    [Column("correo")]
    public string Correo { get; set; }

    [Column("sueldo")]
    public decimal Sueldo { get; set; }

    [Column("tipoContrato")]
    public string TipoContrato { get; set; }
}
