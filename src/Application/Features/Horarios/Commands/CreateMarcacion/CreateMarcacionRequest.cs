using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Horarios.Commands.CreateMarcacion;

public class CreateMarcacionRequest
{
    [JsonPropertyName("codigoEmpleado")]
    public string CodigoEmpleado { get; set; }

    [JsonPropertyName("dispositivoId")]
    public string DispositivoId { get; set; }

    [JsonPropertyName("localidadId")]
    public string LocalidadId { get; set; }

    //[JsonPropertyName("latitud")]
    //public double Latitud { get; set; }

    //[JsonPropertyName("longitud")]
    //public double Longitud { get; set; }

}
