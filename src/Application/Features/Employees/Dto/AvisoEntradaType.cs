using System.Text.Json.Serialization;

namespace EnrolApp.Application.Features.Employees.Dto;
public class AvisoEntradaType
{
    [JsonPropertyName("tipoNovedad")]
    public string TipoNovedad { get; set; }

    [JsonPropertyName("codigoEmpleado")]
    public int CodigoEmpleado { get; set; }

    [JsonPropertyName("identificacion")]
    public string Identificacion { get; set; }

    [JsonPropertyName("tipoIdentificacion")]
    public string TipoIdentificacion { get; set; }

    [JsonPropertyName("nombres")]
    public string Nombres { get; set; }

    [JsonPropertyName("apellidos")]
    public string Apellidos { get; set; }

    [JsonPropertyName("direccionEmpleado")]
    public string DireccionEmpleado { get; set; }

    [JsonPropertyName("fechaIngreso")]
    public DateTime FechaIngreso { get; set; }

    [JsonPropertyName("tipoContrato")]
    public string TipoContrato { get; set; }

    [JsonPropertyName("actividadSectorial")]
    public string ActividadSectorial { get; set; }

    [JsonPropertyName("cargo")]
    public string Cargo { get; set; }

    [JsonPropertyName("sueldo")]
    public decimal Sueldo { get; set; }

    [JsonPropertyName("aportacionNominal")]
    public string AportacionNominal { get; set; }

    [JsonPropertyName("representanteLegal")]
    public string RepresentanteLegal { get; set; }

    [JsonPropertyName("empresa")]
    public string Empresa { get; set; }

    [JsonPropertyName("rucEmpresa")]
    public string RucEmpresa { get; set; }

    [JsonPropertyName("sucursal")]
    public string Sucursal { get; set; }
}
