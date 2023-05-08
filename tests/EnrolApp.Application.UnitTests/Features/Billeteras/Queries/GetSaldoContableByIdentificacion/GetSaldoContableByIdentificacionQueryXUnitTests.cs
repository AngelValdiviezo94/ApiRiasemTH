using AutoFixture;
using AutoMapper;
using EnrolApp.Application.Common.Mappings;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Billeteras.Dto;
using EnrolApp.Application.Features.Billeteras.Interfaces;
using EnrolApp.Application.Features.Billeteras.Queries.GetSaldoContableByIdentificacion;
using EnrolApp.Domain.Entities.Wallet;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Billeteras.Queries.GetSaldoContableByIdentificacion;

public class GetSaldoContableByIdentificacionQueryXUnitTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ISaldoContable> _mockrepository;

    public GetSaldoContableByIdentificacionQueryXUnitTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mockrepository = new Mock<ISaldoContable>();
        _mapper = mapperConfig.CreateMapper();
    }

    //[Theory]
    //[InlineData("0951635390")]
    //public async void GetSaldoContableByIdentificacionAsync_ReturrnType(string identificacion)
    //{
    //    var fixture = new Fixture();
    //    var cupoCredito = fixture.Create<SaldoContable>();
    //    _mockrepository.Setup(r => r.GetSaldoContableByIdentificacionAsync("0951635390")).ReturnsAsync(cupoCredito);

    //    var handler = new GetSaldoContableByIdentificacionQueryHandler(_mockrepository.Object, _mapper);

    //    var request = new GetSaldoContableByIdentificacionQuery(identificacion);

    //    var result = await handler.Handle(request, CancellationToken.None);

    //    result.ShouldBeOfType<ResponseType<SaldoContableResponseType>>();
    //}

}
