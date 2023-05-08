namespace EnrolApp.Domain.Entities.Nomina;

public class AvisoEntrada
{
    public string TipoNovedad { get; set; }
    public int  CodigoEmpleado { get; set; }
    public string Identificacion { get; set; }
    public string TipoIdentificacion { get; set; }

    public string Nombres { get; set; }
    public string Apellidos { get; set; }

    public string DireccionEmpleado { get; set; }
    public DateTime FechaIngreso { get; set; }
    public string TipoContrato { get; set; }
    public string ActividadSectorial { get; set; }
    public string Cargo { get; set; }
    public decimal Sueldo { get; set; }
    public string AportacionNominal { get; set; }
    public string RepresentanteLegal { get; set; }
    public string Empresa { get; set; }
    public string RucEmpresa { get; set; }

    public string Sucursal { get; set; }


}
