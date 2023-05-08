using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.MenuAlimentacion.Dto
{
    public class MenuSemanaResponseType
    {
        [JsonPropertyName("orgaCodigo")]
        public int OrgaCodigo { get; set; }

        [JsonPropertyName("planCodigo")]
        public int PlanCodigo { get; set; }

        [JsonPropertyName("menuHoy")]
        public MenuAlimentos MenuHoy { get; set; }

        [JsonPropertyName("menuSemana")]
        public SemanaAlimentos MenuSemana { get; set; }

        [JsonPropertyName("menuSemanaAnterior")]
        public SemanaAlimentos MenuSemanaAnterior { get; set; }
    }

    public class MenuAlimentos
    {
        [JsonPropertyName("menuCodigo")]
        public int MenuCodigo { get; set; }

        [JsonPropertyName("menuFecha")]
        public string MenuFecha { get; set; }

        [JsonPropertyName("menuAnio")]
        public int MenuAnio { get; set; }

        [JsonPropertyName("menuSemana")]
        public int MenuSemana { get; set; }

        [JsonPropertyName("numeroDiaSemana")]
        public int NumeroDiaSemana { get; set; }

        [JsonPropertyName("menu")]
        public List<Alimentos> Menu { get; set; }
    }

    public class Alimentos
    {
        [JsonPropertyName("secuencia")]
        public int Secuencia { get; set; }

        [JsonPropertyName("receCodigo")]
        public int ReceCodigo { get; set; }

        [JsonPropertyName("receImagen")]
        public string ReceImagen { get; set; }

        [JsonPropertyName("receNombre")]
        public string ReceNombre { get; set; }

        [JsonPropertyName("tireCodigo")]
        public int TireCodigo { get; set; }

        [JsonPropertyName("tireDescripcion")]
        public string TireDescripcion { get; set; }

        [JsonPropertyName("tiseCodigo")]
        public int TiseCodigo { get; set; }

        [JsonPropertyName("tiseDescripcion")]
        public string TiseDescripcion { get; set; }
    }

    public class SemanaAlimentos
    {
        [JsonPropertyName("menuFechaIni")]
        public string MenuFechaIni { get; set; }

        [JsonPropertyName("menuFechaFin")]
        public string MenuFechaFin { get; set; }

        [JsonPropertyName("dias")]
        public List<MenuAlimentos> Dias { get; set; }       

    }
}
