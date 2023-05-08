using Ardalis.Specification.EntityFrameworkCore;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Persistence.Repository.General;

public class CustomRepositoryMarcacionesAsync<T> : RepositoryBase<T>, IRepositoryMarcacionesAsync<T> where T : class
{
    private readonly ApplicationsDbMarcacionesContext dbContext;

    public CustomRepositoryMarcacionesAsync(ApplicationsDbMarcacionesContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }
}
