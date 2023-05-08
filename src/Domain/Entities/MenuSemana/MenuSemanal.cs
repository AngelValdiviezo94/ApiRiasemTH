using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Domain.Entities.MenuSemana;

public class MenuSemanal
{
    public int OrgaCodigo { get; set; }

    public int PlanCodigo { get; set; }

    public MenuAlimentos MenuHoy { get; set; }

    public SemanaAlimentos MenuSemana { get; set; }

    public SemanaAlimentos MenuSemanaAnterior { get; set; }
}

public class MenuAlimentos
{
    public int MenuCodigo { get; set; }

    public string MenuFecha { get; set; }

    public int MenuAnio { get; set; }

    public int MenuSemana { get; set; }

    public int NumeroDiaSemana { get; set; }

    public List<Alimentos> Menu { get; set; }
}

public class Alimentos
{
    public int Secuencia { get; set; }

    public int ReceCodigo { get; set; }

    public string ReceImagen { get; set; }

    public string ReceNombre { get; set; }

    public int TireCodigo { get; set; }

    public string TireDescripcion { get; set; }

    public int TiseCodigo { get; set; }

    public string TiseDescripcion { get; set; }
}

public class SemanaAlimentos
{
    public string MenuFechaIni { get; set; }

    public string MenuFechaFin { get; set; }

    public List<MenuAlimentos> Dias { get; set; }

}
