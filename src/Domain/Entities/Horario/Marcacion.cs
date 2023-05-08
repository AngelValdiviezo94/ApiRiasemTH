using EnrolApp.Domain.Entities.Organizacion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnrolApp.Domain.Entities.Horario;

public class Marcacion
{
    [Key]
    [Column("checkMarcacion")]
    public DateTime? CheckMarcacion { get; set; }

    [Column("idMarcacion")]
    public int IdMarcacion { get; set; }

    [Column("tipoMarcacion")]
    public int TipoMarcacion { get; set; }

    [Column("codigoBiometrico")]
    public string CodigoBiometrico { get; set; }

}
