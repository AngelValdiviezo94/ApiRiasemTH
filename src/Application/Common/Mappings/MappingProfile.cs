using AutoMapper;
using EnrolApp.Application.Features.Billeteras.Dto;
using EnrolApp.Application.Features.Clients.Commands.CreateCliente;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Application.Features.Employees.Dto;
using EnrolApp.Application.Features.Familiares.Dto;
using EnrolApp.Application.Features.Horarios.Dto;
using EnrolApp.Application.Features.MenuAlimentacion.Dto;
using EnrolApp.Application.Features.Wallets.Dto;
using EnrolApp.Domain.Dto;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Familiares;
using EnrolApp.Domain.Entities.Horario;
using EnrolApp.Domain.Entities.MenuSemana;
using EnrolApp.Domain.Entities.Nomina;
using EnrolApp.Domain.Entities.Wallet;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace EnrolApp.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateClienteRequest, Cliente>(MemberList.None); 
        CreateMap<Cliente, ClienteType>(); 
        CreateMap<FamiliarColaborador, ClienteType>(); 
        CreateMap<InformacionGeneralEmpleado,InformacionGeneralEmpleadoType>().ReverseMap();
        CreateMap<CertificadoLaboral, CertificadoLaboralType>().ReverseMap();
        CreateMap<AvisoEntrada, AvisoEntradaType>().ReverseMap();
        
        CreateMap<RolPagoCabecera, RolPagoCabeceraType>();
        CreateMap<Rubro, RubroType>();
        CreateMap<RolPago, RolPagoType>();
        CreateMap<Horario, HorarioType>();
        CreateMap<CupoCredito, CupoCreditoResponseType>();
        CreateMap<SaldoContable, SaldoContableResponseType>();
        CreateMap<MenuSemanal, MenuSemanaResponseType>().ReverseMap();
        CreateMap<TipoRelacionFamiliar, ResponseTipoRelacionFamiliarType>();
        CreateMap<FamiliarColaborador, ResponseFamiliarColaboradorType>();

        //ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    [ExcludeFromCodeCoverage]
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);

        var mappingMethodName = nameof(IMapFrom<object>.Mapping);
        [ExcludeFromCodeCoverage]
        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                {
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}