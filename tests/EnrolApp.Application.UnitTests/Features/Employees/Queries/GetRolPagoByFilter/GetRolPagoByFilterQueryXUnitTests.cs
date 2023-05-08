using AutoFixture;
using AutoMapper;
using EnrolApp.Application.Common.Mappings;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Employees.Dto;
using EnrolApp.Application.Features.Employees.Interfaces;
using EnrolApp.Application.Features.Employees.Queries.GetRolPagoByFilter;
using EnrolApp.Domain.Entities.Nomina;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Employees.Queries.GetRolPagoByFilter;

public class GetRolPagoByFilterQueryXUnitTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IReportesEmpleado> _mockrepository;
    public GetRolPagoByFilterQueryXUnitTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockrepository = new Mock<IReportesEmpleado>();

    }

    //[Theory]
    //[InlineData("0951635390","2022-05-15")]
    //public async void GetRolPagoByFilterAsync_ReturrnDataCertificadoLaboral(string identificacion,string fechaCorte)
    //{
    //    var fixture = new Fixture();
    //    var rolPago = fixture.Create<RolPago>();
    //    _mockrepository.Setup(r => r.GetRolPagoByFilterAsync("0951635390","2022-05-15")).ReturnsAsync(rolPago);

    //    var handler = new GetRolPagoByFilterQueryHandler(_mockrepository.Object, _mapper);

    //    var request = new GetRolPagoByFilterQuery(identificacion,fechaCorte);

    //    var result = await handler.Handle(request, CancellationToken.None);

    //    result.ShouldBeOfType<ResponseType<RolPagoType>>();
    //}
}
