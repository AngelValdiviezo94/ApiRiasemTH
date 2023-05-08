using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Nomina;

public class CertificadoLaboral : InformacionGeneralEmpleado
{
   
    [Column("grupoCorporativoRrhh")]
    public string GrupoCorporativoRrhh { get; set; }

    [Column("cargoCorporativoRrhh")]
    public string CargoCorporativoRrhh { get; set; }

    [Column("encargadoCorporativoRrhh")]
    public string EncargadoCorporativoRrhh { get; set; }


}
