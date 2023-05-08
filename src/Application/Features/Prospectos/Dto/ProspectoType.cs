
using System.Text.Json.Serialization;
namespace EnrolApp.Application.Features.Prospectos.Dto;

public class ProspectoType
{
   
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("tipoIdentificacion")]
    public string TipoIdentificacion { get; set; }

    [JsonPropertyName("identificacion")]
    public string Identificacion { get; set; }

    [JsonPropertyName("nombres")]
    public string Nombres { get; set; }

    [JsonPropertyName("apellidos")]
    public string Apellidos { get; set; }

    [JsonPropertyName("alias")]
    public string Alias { get; set; }

    [JsonPropertyName("celular")]
    public string Celular { get; set; }

    [JsonPropertyName("grupoEmpresarial")]
    public string GrupoEmpresarial { get; set; }

    [JsonPropertyName("empresa")]
    public string Empresa { get; set; }

    [JsonPropertyName("codigoEmpresa")]
    public string CodigoEmpresa { get; set; }

    [JsonPropertyName("area")]
    public string Area { get; set; }

    [JsonPropertyName("departamento")]
    public string Departamento { get; set; }

    [JsonPropertyName("fechaNacimiento")]
    public string FechaNacimiento { get; set; }

    [JsonPropertyName("genero")]
    public string Genero { get; set; }

    [JsonPropertyName("direccion")]
    public string Direccion { get; set; }

    [JsonPropertyName("latitud")]
    public float Latitud { get; set; }

    [JsonPropertyName("longitud")]
    public float Longitud { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }
}
