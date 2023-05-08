using System.Text.Json.Serialization;

namespace EnrolApp.Application.Features.Employees.Dto;

public class CertificadoLaboralType : InformacionGeneralEmpleadoType
{

    [JsonPropertyName("grupoCorporativoRrhh")]
    public string GrupoCorporativoRrhh { get; set; }

    [JsonPropertyName("cargoCorporativoRrhh")]
    public string CargoCorporativoRrhh { get; set; }

    [JsonPropertyName("encargadoCorporativoRrhh")]
    public string EncargadoCorporativoRrhh { get; set; }

}
