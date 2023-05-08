using EnrolApp.Application.Features.Clients.Dto;
using System.Text.Json.Serialization;

namespace EnrolApp.Application.Features.Clients.Commands.CreateCliente
{
    public class CreateClienteRequest 
    {

        [JsonPropertyName("tipoIdentificacion")]
        public string TipoIdentificacion { get; set; }

        [JsonPropertyName("identificacion")]
        public string Identificacion { get; set; }

        [JsonPropertyName("fechaNacimiento")]
        public string FechaNacimiento { get; set; }

        [JsonPropertyName("genero")]
        public string Genero { get; set; }

        [JsonPropertyName("direccion")]
        public string Direccion { get; set; }

        [JsonPropertyName("latitud")]
        public string Latitud { get; set; }

        [JsonPropertyName("longitud")]
        public string Longitud { get; set; }

        [JsonPropertyName("correo")]
        public string Correo { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("dispositivoId")]
        public string DispositivoId { get; set; }

        [JsonPropertyName("imagenPerfil")]
        public ImagenPerfilAdjunto Adjunto { get; set; }
        
        [JsonPropertyName("tipoCliente")]
        public string? TipoCliente { get; set; }
    }
}
