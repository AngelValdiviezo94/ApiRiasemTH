
using System.Text.Json.Serialization;


namespace EnrolApp.Application.Features.Clients.Dto
{
    public class ClienteType
    {
      
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("codigoIntegracion")]
        public string CodigoIntegracion { get; set; }
        
        [JsonPropertyName("codigoConvivencia")]
        public string CodigoConvivencia { get; set; }

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

        [JsonPropertyName("latitud")]
        public float Latitud { get; set; }

        [JsonPropertyName("longitud")]
        public float Longitud { get; set; }

        [JsonPropertyName("direccion")]
        public string Direccion { get; set; }

        [JsonPropertyName("celular")]
        public string Celular { get; set; }

        [JsonPropertyName("tipoIdentificacionFamiliar")]
        public string TipoIdentificacionFamiliar { get; set; }

        [JsonPropertyName("IndentificacionFamiliar")]
        public string IndentificacionFamiliar { get; set; }

        [JsonPropertyName("Correo")]
        public string Correo { get; set; }

        [JsonPropertyName("fechaRegistro")]
        public DateTime FechaRegistro { get; set; }

        [JsonPropertyName("servicioActivo")]
        public bool ServicioActivo { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; }

        [JsonPropertyName("sesionColaborador")]
        public string SesionColaborador { get; set; }
    }
}
