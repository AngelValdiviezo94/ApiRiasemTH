using EnrolApp.Application.Features.Clients.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Employees.Dto
{
    public class EmpleadoCargoType
    {
        public Guid Id { get; set; }
        public int CodigoEmpleado { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }

        
        public string CodEmpresa { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Area { get; set; }
        public string RucEmpresa { get; set; }
        public string GrupoEmpresarial { get; set; }
        public string Correo { get; set; }
        public decimal Sueldo { get; set; }
        public string TipoContrato { get; set; }
        public string DispositivoId { get; set; }
        public double LatitudLocalidad { get; set; }
        public double LongitudLocalidad { get; set; }       
        public CargoType CargoSuscriptor { get; set; }
    }
}
