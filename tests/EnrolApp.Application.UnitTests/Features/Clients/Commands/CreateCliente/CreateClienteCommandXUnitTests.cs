using Ardalis.Specification;
using AutoFixture;
using AutoMapper;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Mappings;
using EnrolApp.Application.Features.Clients.Commands.CreateCliente;
using EnrolApp.Application.Features.Prospectos.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Suscripcion;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace EnrolApp.Application.UnitTests.Features.Clients.Commands.CreateCliente;

public class CreateClienteCommandXUnitTests
{
    private readonly Mock<IRepositoryAsync<Cliente>> _mocRepositoriokCl;
    private readonly Mock<IRepositoryAsync<Prospecto>> _mocRepositoriokPr;
    private readonly Mock<IMapper> _mockmapper;
    private readonly Mock<IApisConsumoAsync> _mockrepositoryApis;
    private readonly Mock<IConfiguration> _mockConfig;

    public CreateClienteCommandXUnitTests()
    {
        _mocRepositoriokCl = new Mock<IRepositoryAsync<Cliente>>();
        _mocRepositoriokPr = new Mock<IRepositoryAsync<Prospecto>>();
        _mockmapper = new Mock<IMapper>();
        _mockrepositoryApis = new Mock<IApisConsumoAsync>();
        _mockConfig = new Mock<IConfiguration>();

    }

    //[Fact]
    //public async void CreateClienteCommand_ReturnTrueCrearCliente()
    //{
    //    #region Arrange
    //    var fixture = new Fixture();
    //    fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    //    fixture.OmitAutoProperties = true;
    //    var clienteRequest= fixture.Build<CreateClienteRequest>()
    //                    .With(r=> r.Identificacion,"0951635390")
    //                    .Create();
    //    var cliente = fixture.Build<Cliente>()
    //        .With(r => r.Identificacion, "0951635390")
    //        .Create();
    //    var request = new CreateClienteCommand(clienteRequest);
    //    var prospecto = fixture.Build<Prospecto>().With(r => r.Identificacion, "0951635390")
    //        .With(r => r.TipoRelacion, fixture.Build<TipoRelacion>().With(x => x.Codigo, "P").Create())
    //        .Create();
    //    var endPoint = fixture.Build<(bool Success, object Data)>()
    //                .With(r => r.Success, true)
    //                .Create();
    //    var resultCorreo = fixture.Build<(bool Success, object Data)>()
    //                .With(r => r.Success, true)
    //                .Create();
    //    var mockConfigurationSection = new Mock<IConfigurationSection>();
    //    mockConfigurationSection.Setup(r => r.Path).Returns("Rootvalue");
    //    mockConfigurationSection.Setup(r => r.Key).Returns("RootvalueLocall");
    //    mockConfigurationSection.Setup(r => r.Value).Returns("0.15");
    //    _mockmapper.Setup(r => r.Map<Cliente>(request.ClienteRequest)).Returns(cliente);
    //    _mockConfig.Setup(con => con.GetSection("EndPointConsumoApis:ApiAuth:ActualizarUsuarioLdap")).Returns(mockConfigurationSection.Object);
    //    _mockConfig.Setup(con => con.GetSection("Notificaciones:PlantillaActivaServicio")).Returns(mockConfigurationSection.Object);
    //    _mockConfig.Setup(con => con.GetSection("Notificaciones:AsuntoActivaServicio")).Returns(mockConfigurationSection.Object);
    //    _mockConfig.Setup(con => con.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo")).Returns(mockConfigurationSection.Object);
    //    _mockConfig.Setup(con => con.GetSection("ConsumoApis:UrlBaseApiUtils")).Returns(mockConfigurationSection.Object);
    //    _mockConfig.Setup(con => con.GetSection("ConsumoApis:UrlBaseApiAuth")).Returns(mockConfigurationSection.Object);
    //    _mocRepositoriokPr.Setup(r => r.FirstOrDefaultAsync(It.IsAny<ProspectoByIdentificacionSpec>(), CancellationToken.None)).ReturnsAsync(prospecto);
    //    _mocRepositoriokCl.Setup(r => r.AddAsync(cliente, CancellationToken.None)).ReturnsAsync(cliente);
    //    _mockrepositoryApis.Setup(r => r.PostEndPoint(It.IsAny<object>() , "0.150.15", "0.15")).ReturnsAsync((endPoint));
    //    _mockrepositoryApis.Setup(r => r.PostEndPoint(It.IsAny<object>(), "0.150.15", "0.15")).ReturnsAsync((resultCorreo));
    //    #endregion

    //    #region Act
    //    var handler = new CreateClienteCommandHandler(_mocRepositoriokCl.Object, _mocRepositoriokPr.Object, _mockrepositoryApis.Object, _mockConfig.Object, _mockmapper.Object);

    //    var result = await handler.Handle(request, CancellationToken.None);
    //    #endregion

    //    #region Assert
    //    result.Succeeded.ShouldBeTrue();
    //    #endregion




    //}
}
