using System.Text.Json.Serialization;

namespace EnrolApp.Application.Features.Familiares.Dto
{
    public class ResponseTipoRelacionFamiliarType
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
    }
}
