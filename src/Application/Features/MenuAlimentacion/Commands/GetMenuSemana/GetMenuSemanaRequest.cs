using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.MenuAlimentacion.Commands.GetMenuSemana
{
    public class GetMenuSemanaRequest
    {
        [JsonPropertyName("identificacion")]
        public string Identificacion { get; set; }

        [JsonPropertyName("orgaCodigo")]
        public string OrgaCodigo { get; set; }
    }
}
