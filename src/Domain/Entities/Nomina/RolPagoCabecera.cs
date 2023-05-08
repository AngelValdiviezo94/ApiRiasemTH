using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Nomina;

public class RolPagoCabecera
{

    [Column("nombres")]
    public string Nombres { get; set; }

    [Column("apellidos")]
    public string Apellidos { get; set; }

    [Column("division")]
    public string Division { get; set; }

    [Column("empresa")]
    public string Empresa { get; set; }

    [Column("sucursal")]
    public string Sucursal { get; set; }

    [Column("tipoNomina")]
    public string TipoNomina { get; set; }

    [Column("proceso")]
    public string Proceso { get; set; }

    [Column("periodo")]
    public string Periodo { get; set; }

    [Column("area")]
    public string Area { get; set; }

    [Column("centroCosto")]
    public string CentroCosto { get; set; }

    [Column("subCentroCosto")]
    public string SubCentroCosto { get; set; }

    [Column("cargo")]
    public string Cargo { get; set; }

    [Column("sueldo")]
    public decimal Sueldo { get; set; }

    [Column("tipoPago")]
    public string TipoPago { get; set; }

    [Column("encargadoCoporativoRRHH")]
    public string EncargadoCorporativoRRHH { get; set; }

    [Column("cargoCorporativoRRHH")]
    public string CargoCoporativoRRHH { get; set; }

    [Column("observacion")]
    public string Observacion { get; set; }
}
