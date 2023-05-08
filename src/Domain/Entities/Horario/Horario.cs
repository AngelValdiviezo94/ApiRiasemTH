namespace EnrolApp.Domain.Entities.Horario;

public class Horario
{
    public DateTime? Fecha { get; set; } //PANA
    public int? IdMarcacionE { get; set; }

    public int? IdMarcacionS { get; set; }

    public DateTime? MarcacionEntrada { get; set; } // EVENTO 10

    public DateTime? MarcacionSalida { get; set; } // EVENTO 11

    public DateTime? MarcacionEntradaReceso { get; set; } // evento 14

    public DateTime? MarcacionSalidaReceso { get; set; } // evento 15

    public Guid? IdTurno { get; set; }
    //public int? IdTurno { get; set; } // ID TURNO PROVENIENTE DE PANACEA
    public DateTime? TurnoEntrada { get; set; } //PANA

    public DateTime? TurnoSalida { get; set; }  //PANA 

    public DateTime? Receso { get; set; } //PANA

    public string CodigoBiometrico { get; set; } // MARCACIONES

    public string TipoTurno { get; set; } // diurno / nocturno PANA

    public string CodigoTurno { get; set; } //PANA

    public string DescripcionTurno { get; set; } //PANA

    public string CodigoResultadoEvaluacion { get; set; } //PANA

    public string DescripcionResultadoEvaluacion { get; set; } //PANA

}
