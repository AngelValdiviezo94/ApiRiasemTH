using System.Reflection;
using EnrolApp.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EnrolApp.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

       

        return services;
    }
}
