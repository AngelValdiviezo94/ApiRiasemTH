using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnrolApp.Shared;
public static class ServiceExtensions
{
    public static IServiceCollection AddSharedInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
       
        services.AddTransient<IDateTimeService, DateTimeService>();
        return services;
    }
}