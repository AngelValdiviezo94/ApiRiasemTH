using System.Text.Json.Serialization;

namespace EnrolApp.Application.Features.Employees.Dto;

public class InformacionGeneralEmpleadoType
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("codigoEmpleado")]
    public int CodigoEmpleado { get; set; }

    [JsonPropertyName("tipoIdentificacion")]
    public string TipoIdentificacion { get; set; }

    [JsonPropertyName("identificacion")]
    public string Identificacion { get; set; }

    [JsonPropertyName("nombres")]
    public string Nombres { get; set; }

    [JsonPropertyName("apellidos")]
    public string Apellidos { get; set; }

    [JsonPropertyName("empresa")]
    public string Empresa { get; set; }

    [JsonPropertyName("cargo")]
    public string Cargo { get; set; }

    [JsonPropertyName("codEmpresa")]
    public string Cod_Empresa { get; set; }

    [JsonPropertyName("fechaIngreso")]
    public DateTime FechaIngreso { get; set; }

    [JsonPropertyName("area")]
    public string Area { get; set; }

    [JsonPropertyName("rucEmpresa")]
    public string RucEmpresa { get; set; }

    [JsonPropertyName("grupoEmpresarial")]
    public string GrupoEmpresarial { get; set; }

    [JsonPropertyName("correo")]
    public string Correo { get; set; }

    [JsonPropertyName("sueldo")]
    public decimal Sueldo { get; set; }

    [JsonPropertyName("tipoContrato")]
    public string TipoContrato { get; set; }

    [JsonPropertyName("dispositivoId")]
    public string DispositivoId { get; set; }

    [JsonPropertyName("latitudLocalidad")]
    public double LatitudLocalidad { get; set; }

    [JsonPropertyName("longitudLocalidad")]
    public double LongitudLocalidad { get; set; }

}
