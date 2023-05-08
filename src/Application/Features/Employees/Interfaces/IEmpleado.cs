using EnrolApp.Domain.Entities.Nomina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Employees.Interfaces;

public interface IEmpleado
{
    //InformacionGeneralEmpleado GetInfoGeneralByIdentificacion(string Identificacion);

    Task<InformacionGeneralEmpleado> GetInfoGeneralByIdentificacion(string Identificacion);
}
