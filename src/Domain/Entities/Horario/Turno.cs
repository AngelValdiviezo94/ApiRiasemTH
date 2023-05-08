using EnrolApp.Domain.Entities.Organizacion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnrolApp.Domain.Entities.Horario;

public class Turno
{
    
    [Key]
    [Column("fecha")]
    public DateTime? Fecha { get; set; }

    [Column("idTurno")]
    public int IdTurno { get; set; }

    [Column("codigoTurno")]
    public string CodigoTurno { get; set; }

    [Column("descripcionTurno")]
    public string DescripcionTurno { get; set; }

    [Column("tipoTurno")]
    public string TipoTurno { get; set; }

    [Column("turnoEntrada")]
    public DateTime? TurnoEntrada { get; set; }

    [Column("turnoSalida")]
    public DateTime? TurnoSalida { get; set; }

    [Column("receso")]
    public DateTime? Receso { get; set; }
}
