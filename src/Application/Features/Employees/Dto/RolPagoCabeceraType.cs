using System.Text.Json.Serialization;

namespace EnrolApp.Application.Features.Employees.Dto;

public class RolPagoCabeceraType
{
    [JsonPropertyName("nombres")]
    public string Nombres { get; set; }

    [JsonPropertyName("apellidos")]
    public string Apellidos { get; set; }

    [JsonPropertyName("division")]
    public string Division { get; set; }

    [JsonPropertyName("empresa")]
    public string Empresa { get; set; }

    [JsonPropertyName("sucursal")]
    public string Sucursal { get; set; }

    [JsonPropertyName("tipoNomina")]
    public string TipoNomina { get; set; }

    [JsonPropertyName("proceso")]
    public string Proceso { get; set; }

    [JsonPropertyName("periodo")]
    public string Periodo { get; set; }

    [JsonPropertyName("area")]
    public string Area { get; set; }

    [JsonPropertyName("centroCosto")]
    public string CentroCosto { get; set; }

    [JsonPropertyName("subCentroCosto")]
    public string SubCentroCosto { get; set; }

    [JsonPropertyName("cargo")]
    public string Cargo { get; set; }

    [JsonPropertyName("sueldo")]
    public decimal Sueldo { get; set; }

    [JsonPropertyName("encargadoCoporativoRRHH")]
    public string EncargadoCorporativoRRHH { get; set; }

    [JsonPropertyName("cargoCorporativoRRHH")]
    public string CargoCoporativoRRHH { get; set; }

    [JsonPropertyName("tipoPago")]
    public string TipoPago { get; set; }

    [JsonPropertyName("observacion")]
    public string Observacion { get; set; }
}
