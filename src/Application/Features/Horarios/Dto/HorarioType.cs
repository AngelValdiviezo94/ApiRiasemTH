using System.Text.Json.Serialization;


namespace EnrolApp.Application.Features.Horarios.Dto
{
    public class HorarioType
    {

        [JsonPropertyName("fecha")]
        public DateTime? Fecha { get; set; }

        [JsonPropertyName("idMarcacionE")]
        public int IdMarcacionE { get; set; }

        [JsonPropertyName("idMarcacionS")]
        public int IdMarcacionS { get; set; }

        [JsonPropertyName("marcacionEntrada")]
        public DateTime? MarcacionEntrada { get; set; }

        [JsonPropertyName("marcacionSalida")]
        public DateTime? MarcacionSalida { get; set; }

        [JsonPropertyName("marcacionEntradaReceso")]
        public DateTime? MarcacionEntradaReceso { get; set; }

        [JsonPropertyName("marcacionSalidaReceso")]
        public DateTime? MarcacionSalidaReceso { get; set; }

        [JsonPropertyName("idTurno")]
        public int IdTurno { get; set; }

        [JsonPropertyName("turnoEntrada")]
        public DateTime? TurnoEntrada { get; set; }

        [JsonPropertyName("turnoSalida")]
        public DateTime? TurnoSalida { get; set; }

        [JsonPropertyName("receso")]
        public DateTime? Receso { get; set; }

        [JsonPropertyName("codigoBiometrico")]
        public string CodigoBiometrico { get; set; } // MARCACIONES

        [JsonPropertyName("tipoTurno")]
        public string TipoTurno { get; set; } // diurno / nocturno PANA

        [JsonPropertyName("codigoTurno")]
        public string CodigoTurno { get; set; } //PANA

        [JsonPropertyName("descripcionTurno")]
        public string DescripcionTurno { get; set; } //PANA

        [JsonPropertyName("codResultEvaluacion")]
        public string CodigoResultadoEvaluacion { get; set; }

        [JsonPropertyName("descResultEvaluacion")]
        public string DescripcionResultadoEvaluacion { get; set; }
    }
}
