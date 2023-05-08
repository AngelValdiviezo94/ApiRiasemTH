using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EnrolApp.Domain.Entities.Nomina;

[Table("NM_Empleado", Schema = "dbo")]
public class Empleado : InformacionGeneralEmpleado
{
    [ExcludeFromCodeCoverage]
    [Column("id")]
    public Guid Id { get; set; }

    //[Column("codigoEmpleado")]
    //public int CodigoEmpleado { get; set; }

}
