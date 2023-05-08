using AutoFixture;
using AutoMapper;
using EnrolApp.Application.Common.Mappings;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Employees.Dto;
using EnrolApp.Application.Features.Employees.Interfaces;
using EnrolApp.Application.Features.Employees.Queries.GetInfoGeneralByIdentificacion;
using EnrolApp.Domain.Entities.Nomina;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Employees.Queries.GetInfoGeneralByIdentificacion
{
    public class GetInfoGeneralByIdentificacionQueryXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEmpleado> _mockrepository;
        public GetInfoGeneralByIdentificacionQueryXUnitTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockrepository = new Mock<IEmpleado>();

        }
        //[Theory]
        //[InlineData("0951635390")]
        //public async void GetInfoGeneralByIdentificacionQuery_ReturrnDataInformacionEmpleado(string identificacion)
        //{
        //    var fixture = new Fixture();
        //    var certificado = fixture.Create<InformacionGeneralEmpleado>();
        //    _mockrepository.Setup(r => r.GetInfoGeneralByIdentificacion("0951635390")).ReturnsAsync(certificado);

        //    var handler = new GetInfoGeneralByIdentificacionQueryHandler(_mockrepository.Object, _mapper);

        //    var request = new GetInfoGeneralByIdentificacionQuery(identificacion);

        //    var result = await handler.Handle(request, CancellationToken.None);

        //    result.ShouldBeOfType<ResponseType<InformacionGeneralEmpleadoType>>();
        //    result.Succeeded.ShouldBeTrue();
        //    //result.Data.ShouldBeNull("No Existe el numero: " + identificacion.ToString());

        //}

    }
}
