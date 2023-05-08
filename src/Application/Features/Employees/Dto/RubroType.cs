using System.Text.Json.Serialization;

namespace EnrolApp.Application.Features.Employees.Dto;

public class RubroType
{
    [JsonIgnore]
    public int Id { get; set; }

    [JsonPropertyName("descripcion")]
    public string Descripcion { get; set; }

    [JsonPropertyName("cantidad")]
    public decimal Cantidad { get; set; }

    [JsonPropertyName("valor")]
    public decimal Valor { get; set; }

    [JsonPropertyName("tipoRubro")]
    public string TipoRubro { get; set; }
}
