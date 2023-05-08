using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Clients.Dto
{
    public class SuscriptorRestableceContrasenaType
    {
        [JsonPropertyName("id")] 
        public Guid Id { get; set; }

        [JsonPropertyName("Nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("identificacion")]
        public string Identificacion { get; set; }

        [JsonPropertyName("correo")]
        public string Correo { get; set; }
        
        [JsonPropertyName("celular")]
        public string Celular { get; set; }
    }
}
