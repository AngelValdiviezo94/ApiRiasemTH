using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Nomina;

public class RolPago
{

    public RolPagoCabecera CabeceraRol { get; set; } 

    public decimal TotalIngresos { get; set; }
    public decimal TotalEgresos { get; set; }
    public decimal NetoPagar { get; set; }

    public List<Rubro> ListaIngresos { get; set; } = new List<Rubro>();
    public List<Rubro> ListaEgresos { get; set; } = new List<Rubro>();

}
