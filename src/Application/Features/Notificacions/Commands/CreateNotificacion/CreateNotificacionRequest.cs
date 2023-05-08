using System.Text.Json.Serialization;

namespace EnrolApp.Application.Features.Notifications.Commands.CreateNotificacion
{
    public class CreateNotificacionRequest 
    {

        [JsonPropertyName("identificacion")]
        public string Identificacion { get; set; }

        [JsonPropertyName("codigoEvento")]
        public string CodigoEvento { get; set; }

        [JsonPropertyName("listaParamValue")]
        public Dictionary<string, string> ListaParamValue { get; set; } = new Dictionary<string, string>();

    }
}
