using System.Text.Json.Serialization;

namespace EnrolApp.Application.Features.Horarios.Commands.GetHorario
{
    public class GetHorarioRequest 
    {
        [JsonPropertyName("identificacion")]
        public string Identificacion { get; set; }

        [JsonPropertyName("fechaDesde")]
        public string FechaDesde { get; set; }

        [JsonPropertyName("fechaHasta")]
        public string FechaHasta { get; set; }

    }
}
