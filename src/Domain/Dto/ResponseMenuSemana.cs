using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Domain.Dto;

public class ResponseMenuSemana
{
    public int orga_codigo { get; set; }
    public int menu_codigo { get; set; }
    public int bode_codigo { get; set; }
    public int plan_codigo { get; set; }
    public int menu_semana { get; set; }
    public int menu_año { get; set; }
    public int NumMes { get; set; }
    public int NumDia { get; set; }
    public DateTime menu_fecha { get; set; }
    public string Dia { get; set; }
    public int clie_codigo { get; set; }
    public string Cliente { get; set; }
    public int tise_codigo { get; set; }
    public string TipoServicio { get; set; }
    public int tire_tipo { get; set; }
    public string TipoReceta { get; set; }
    public int rece_codigo { get; set; }
    public string rece_nombre { get; set; }
    public string prod_nombreImagen { get; set; }
}
