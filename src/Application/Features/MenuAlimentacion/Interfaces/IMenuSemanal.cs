using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Domain.Entities.MenuSemana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.MenuAlimentacion.Interfaces;

public interface IMenuSemanal
{
    Task<ResponseType<MenuSemanal>> GetMenuSemanaAsync(string identificacion, string codOrganizacion,string token,CancellationToken cancellationToken);
}
