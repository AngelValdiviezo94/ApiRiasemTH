using Ardalis.Specification.EntityFrameworkCore;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Persistence.Contexts;


namespace EnrolApp.Persistence.Repository.General;

public class CustomRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
{
    private readonly ApplicationDbContext dbContext;

    public CustomRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }
}
