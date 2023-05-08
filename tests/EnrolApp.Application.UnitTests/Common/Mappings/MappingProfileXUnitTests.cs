using AutoMapper;
using EnrolApp.Application.Common.Mappings;
using EnrolApp.Application.Features.Billeteras.Dto;
using EnrolApp.Application.Features.Clients.Commands.CreateCliente;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Application.Features.Employees.Dto;
using EnrolApp.Application.Features.Horarios.Dto;
using EnrolApp.Application.Features.Wallets.Dto;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Horario;
using EnrolApp.Domain.Entities.Nomina;
using EnrolApp.Domain.Entities.Wallet;
using System.Runtime.Serialization;
using Xunit;

namespace EnrolApp.Application.UnitTests.Common.Mappings
{
    public class MappingProfileXUnitTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingProfileXUnitTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }
        [Fact]
        public void ShouldBeValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(CreateClienteRequest), typeof(Cliente))]
        [InlineData(typeof(Cliente), typeof(ClienteType))]
        [InlineData(typeof(InformacionGeneralEmpleado), typeof(InformacionGeneralEmpleadoType))]
        [InlineData(typeof(CertificadoLaboral), typeof(CertificadoLaboralType))]
        [InlineData(typeof(AvisoEntrada), typeof(AvisoEntradaType))]
        [InlineData(typeof(RolPagoCabecera), typeof(RolPagoCabeceraType))]
        [InlineData(typeof(Rubro), typeof(RubroType))]
        [InlineData(typeof(RolPago), typeof(RolPagoType))]
        [InlineData(typeof(Horario), typeof(HorarioType))]
        [InlineData(typeof(CupoCredito), typeof(CupoCreditoResponseType))]
        [InlineData(typeof(SaldoContable), typeof(SaldoContableResponseType))]
        public void Map_SourceToDestination_ExistConfiguration(Type origin, Type destination)
        {
            var instance = FormatterServices.GetUninitializedObject(origin);

            _mapper.Map(instance, origin, destination);
        }
    }
}
