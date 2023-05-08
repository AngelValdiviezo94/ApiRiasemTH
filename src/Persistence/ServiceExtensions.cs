
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Features.Billeteras.Interfaces;
using EnrolApp.Application.Features.Clients.Interfaces;
using EnrolApp.Application.Features.Employees.Interfaces;
using EnrolApp.Application.Features.Familiares.Interfaces;
using EnrolApp.Application.Features.Horarios.Interfaces;
using EnrolApp.Application.Features.MenuAlimentacion.Interfaces;
using EnrolApp.Application.Features.Wallets.Interfaces;
using EnrolApp.Persistence.Contexts;
using EnrolApp.Persistence.Repository.Billetera;
using EnrolApp.Persistence.Repository.Colaborador;
using EnrolApp.Persistence.Repository.Employees;
using EnrolApp.Persistence.Repository.Familiares;
using EnrolApp.Persistence.Repository.General;
using EnrolApp.Persistence.Repository.MenuSemana;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EnrolApp.Persistence;
public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        //services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
        //        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
               builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddDbContext<ApplicationsDbPanaceaContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("Bd_Rrhh_Panacea")));

        services.AddDbContext<ApplicationsDbMarcacionesContext>(options => 
                    options.UseSqlServer(configuration.GetConnectionString("Bd_Marcaciones_GRIAMSE")));


        services.AddTransient(typeof(IRepositoryAsync<>),typeof(CustomRepositoryAsync<>));
        services.AddTransient(typeof(IRepositoryMarcacionesAsync<>), typeof(CustomRepositoryMarcacionesAsync<>));
        services.AddTransient<IApisConsumoAsync, ApisConsumoAsync>();
        services.AddTransient<IReportesEmpleado, ReportesEmpleadoService>();
        services.AddTransient<IHorario, HorarioService>();
        services.AddTransient<ICupoCredito, CupoCreditoService>();
        services.AddTransient<ISaldoContable, SaldoContableService>();
        services.AddTransient<IMenuSemanal, MenuSemanalService>();
        services.AddTransient<IEmpleado, EmpleadoService>();
        services.AddTransient<IFamiliares, FamiliaresService>();
        services.AddTransient<IColaborador, ColaboradorService>();

        return services;
    }
}