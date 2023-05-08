using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using AutoMapper;
using Moq;
using EnrolApp.Application.Features.Employees.Interfaces;
using EnrolApp.Application.Common.Mappings;
using EnrolApp.Domain.Entities.Nomina;
using EnrolApp.Application.Features.Employees.Queries.GetAvisoEntradaByIdentificacion;
using AutoFixture;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Employees.Dto;

namespace EnrolApp.Application.UnitTests.Features.Employees.Queries.GetAvisoEntradaByIdentificacion;

public class GetAvisoEntradaByIdentificacionQueryXUnitTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IReportesEmpleado> _mockrepository;
    public GetAvisoEntradaByIdentificacionQueryXUnitTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockrepository = new Mock<IReportesEmpleado>();

    }

    //[Theory]
    //[InlineData("0951635390")]
    //public async void GetAvisoEntradaByIdentificacionAsync_ReturrnDataAvisoEntrada(string identificacion)
    //{
    //    var fixture = new Fixture();
    //    var certificado = fixture.Create<AvisoEntrada>();
    //    _mockrepository.Setup(r => r.GetAvisoEntradaByIdentificacionAsync("0951635390")).ReturnsAsync(certificado);

    //    var handler = new GetAvisoEntradaByIdentificacionQueryHandler(_mockrepository.Object, _mapper);

    //    var request = new GetAvisoEntradaByIdentificacionQuery(identificacion);

    //    var result = await handler.Handle(request, CancellationToken.None);

    //    result.ShouldBeOfType<ResponseType<AvisoEntradaType>>();
    //}
}
