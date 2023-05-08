using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Domain.Entities.Nomina;

public class Rubro
{

    public int Id { get; set; }
    public string Descripcion { get; set; }
    public decimal Cantidad { get; set; }
    public decimal Valor { get; set; }
    public string TipoRubro { get; set; }
}
