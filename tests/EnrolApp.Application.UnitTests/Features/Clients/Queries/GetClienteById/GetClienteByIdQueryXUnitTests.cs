using AutoFixture;
using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Application.Features.Clients.Queries.GetClienteById;
using EnrolApp.Domain.Entities.Common;
using Moq;
using Shouldly;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Clients.Queries.GetClienteById;

public class GetClienteByIdQueryXUnitTests
{
    private readonly Mock<IRepositoryAsync<Cliente>> _mockRepositoryAsync;
    private readonly Mock<IMapper> _mockMapper;

    public GetClienteByIdQueryXUnitTests()
    {
        _mockRepositoryAsync = new Mock<IRepositoryAsync<Cliente>>();
        _mockMapper = new Mock<IMapper>();
    }

    //[Fact]
    //public void GetClienteByIdQuery_ReturnTrueObtenerClienteId()
    //{
    //    var fixture = new Fixture();
    //    //fixture.OmitAutoProperties = true;
        
    //    //fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    //    var objCliente = fixture.Build<Cliente>()
    //        .OmitAutoProperties().Create();
    //    var objClientType = fixture.Create<ClienteType>();
    //    var request = fixture.Build<GetProspectoByIdQuery>().With(r => r.Codigo, "1515").Create();

    //   _mockRepositoryAsync.Setup(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(objCliente);


    //    _mockMapper.Setup(r => r.Map<ClienteType>(It.IsAny<object>())).Returns(objClientType);

    //    var handle = new GetClienteByIdQueryHandler(_mockRepositoryAsync.Object, _mockMapper.Object);
    //    var result = handle.Handle(request, CancellationToken.None);

    //    _mockMapper.Verify();
    //    _mockRepositoryAsync.Verify();
    //    result.Result.Succeeded.ShouldBeTrue();

    //}

    //[Fact]
    //public void GetClienteByIdQuery_ReturnNullObtenerClienteId()
    //{
    //    var fixture = new Fixture();
    //    var objCliente = fixture.Build<Cliente>()
    //        .OmitAutoProperties().Create();
    //    var objClientType = fixture.Create<ClienteType>();
    //    var request = fixture.Build<GetProspectoByIdQuery>().With(r => r.Codigo, "1515").Create();

    //    _mockRepositoryAsync.Setup(r => r.GetByIdAsync(2, CancellationToken.None)).ReturnsAsync(objCliente);


    //    _mockMapper.Setup(r => r.Map<ClienteType>(It.IsAny<object>())).Returns(objClientType);

    //    var handle = new GetClienteByIdQueryHandler(_mockRepositoryAsync.Object, _mockMapper.Object);
    //    var result = handle.Handle(request, CancellationToken.None);

    //    _mockMapper.Verify();
    //    _mockRepositoryAsync.Verify();

    //   //Pendiente por validar el Throw
        

        

    //}
}
