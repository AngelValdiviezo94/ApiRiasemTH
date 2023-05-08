using System.Text.Json.Serialization;
namespace EnrolApp.Application.Features.Employees.Dto;

public class RolPagoType
{
    [JsonPropertyName("cabeceraRol")]
    public RolPagoCabeceraType CabeceraRol { get; set; }

    [JsonPropertyName("totalIngresos")]
    public decimal TotalIngresos { get; set; }

    [JsonPropertyName("totalEgresos")]
    public decimal TotalEgresos { get; set; }

    [JsonPropertyName("netoPagar")]
    public decimal NetoPagar { get; set; }

    [JsonPropertyName("listaIngresos")]
    public List<RubroType> ListaIngresos { get; set; } = new List<RubroType>();

    [JsonPropertyName("listaEgresos")]
    public List<RubroType> ListaEgresos { get; set; } = new List<RubroType>();
}
