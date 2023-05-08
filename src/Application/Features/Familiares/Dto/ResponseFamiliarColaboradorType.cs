using EnrolApp.Domain.Entities.Common;
using System.Text.Json.Serialization;

namespace EnrolApp.Application.Features.Familiares.Dto
{
    public class ResponseFamiliarColaboradorType
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("tipoRelacionFamiliarId")]
        public Guid TipoRelacionFamiliarId { get; set; }

        [JsonPropertyName("tipoRelacionFamiliarDes")]
        public string TipoRelacionFamiliarDes { get; set; }

        [JsonPropertyName("colaboradorId")]
        public Guid ColaboradorId { get; set; }

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
        public float? Latitud { get; set; }

        [JsonPropertyName("longitud")]
        public double? Longitud { get; set; }

        [JsonPropertyName("direccion")]
        public string Direccion { get; set; }

        [JsonPropertyName("celular")]
        public string Celular { get; set; }

        [JsonPropertyName("correo")]
        public string Correo { get; set; }

        [JsonPropertyName("fechaNacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [JsonPropertyName("genero")]
        public string Genero { get; set; }

        [JsonPropertyName("servicioActivo")]
        public bool ServicioActivo { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; }

        [JsonPropertyName("fechaDesde")]
        public DateTime FechaDesde { get; set; }

        [JsonPropertyName("fechaHasta")]
        public DateTime? FechaHasta { get; set; }

        [JsonPropertyName("habilitado")]
        public bool Habilitado { get; set; }

        [JsonPropertyName("cupo")]
        public double Cupo { get; set; }

        [JsonPropertyName("fotoPerfil")]
        public string FotoPerfil { get; set; }
    }
}
