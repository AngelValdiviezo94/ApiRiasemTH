using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Clients.Dto
{
    public class ResponseCargoType
    {
        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("errors")]
        public object Errors { get; set; }

        [JsonPropertyName("data")]
        public CargoType Data { get; set; } = new();
    }
    
    public class CargoType
    {
        public Guid CargoId { get; set; }
        public string CargoNombre { get; set; }
        public string CargoDescripcion { get; set; }
        public string NombreUsuario { get; set; }
        public List<CanalResponse> Canales { get; set; } = new();
    }

    public class CanalResponse
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<FeatureResponse> Features { get; set; } = new();
    }

    public class FeatureResponse
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<AtributoResponse> Atributos { get; set; } = new();
    }

    public class AtributoResponse
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
