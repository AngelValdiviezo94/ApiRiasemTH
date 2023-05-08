using AutoFixture;
using AutoMapper;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Mappings;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Clients.Commands.ActivateCliente;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Nomina;
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
using EnrolApp.Application.Common.Wrappers;

namespace EnrolApp.Application.UnitTests.Features.Clients.Commands.ActivateCliente;

public class ActivateClienteCommandXUnitTests
{
    private readonly Mock<IRepositoryAsync<Cliente>> _mockRepositoryCl;
    private readonly Mock<IRepositoryAsync<Prospecto>> _mockRepositoryPr;
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly Mock<IApisConsumoAsync> _mockRepositoryApis;
    
    public ActivateClienteCommandXUnitTests()
    {
        _mockRepositoryApis = new Mock<IApisConsumoAsync>();
        _mockConfig = new Mock<IConfiguration>();
        _mockRepositoryCl = new Mock<IRepositoryAsync<Cliente>>();
        _mockRepositoryPr = new Mock<IRepositoryAsync<Prospecto>>();    
    }

    [Theory]
    [InlineData(false,false)]
    [InlineData(true, true)]
    public async void ActivateClienteCommand_ReturnTrueClienteActivado(bool success, bool expect)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        fixture.OmitAutoProperties = true;
        var request = fixture.Build<ActivateClienteCommand>()
            .With(c => c.Identificacion, "MDk1MTYzNTM5MA==")
            .Create();
        var resultEndPoint = fixture.Build<(bool Success, ResponseType<string> obj)>()
            .With(r => r.Success,success)
            .With(r => r.obj, fixture.Build<ResponseType<string>>().With(x => x.StatusCode,"000").Create())
            .Create();

        var objclienteActivo = fixture.Build<Cliente>()
            .With(c => c.ServicioActivo,true)
            .Create();
        var objprospecto2 = fixture.Build<Prospecto>()
            .With(c => c.TipoRelacionId, Guid.Parse("560d4e0c-0af3-4cbd-9ce7-3e09ca90976e" ?? string.Empty))
            .Create();
        var objcliente = fixture.Create<Cliente>();
        var objprospecto = fixture.Create<Prospecto>();
        var mockConfigurationSection = new Mock<IConfigurationSection>();
        var mockConfigurationSectionGuid = new Mock<IConfigurationSection>();
        mockConfigurationSection.Setup(r => r.Value).Returns("0.15");
        mockConfigurationSectionGuid.Setup(r => r.Value).Returns("560d4e0c-0af3-4cbd-9ce7-3e09ca90976e");
        _mockRepositoryApis.Setup(r => r.PostEndPoint(It.IsAny<object>(), "0.150.15", "0.15")).ReturnsAsync(resultEndPoint);
        _mockRepositoryCl.Setup(r => r.FirstOrDefaultAsync(It.IsAny<ClienteByIdentificacionSpec>(), CancellationToken.None)).ReturnsAsync(objcliente);
        _mockRepositoryCl.Setup(cl => cl.UpdateAsync(objclienteActivo, CancellationToken.None));
        _mockRepositoryPr.Setup(pr => pr.FirstOrDefaultAsync(It.IsAny<Application.Features.Prospectos.Specifications.ProspectoByIdentificacionSpec>(), CancellationToken.None)).ReturnsAsync(objprospecto);
        _mockRepositoryPr.Setup(pr => pr.UpdateAsync(objprospecto2, CancellationToken.None));
        _mockConfig.Setup(con => con.GetSection("ConsumoApis:UrlBaseApiAuth")).Returns(mockConfigurationSection.Object);
        _mockConfig.Setup(con => con.GetSection("EndPointConsumoApis:ApiAuth:ActivarUsuarioLdap")).Returns(mockConfigurationSection.Object);
        _mockConfig.Setup(con => con.GetSection("TipoRelacion:codigoUidCliente")).Returns(mockConfigurationSectionGuid.Object);

        var handler = new ActivateClienteCommandHandler(_mockRepositoryCl.Object, _mockRepositoryPr.Object, _mockRepositoryApis.Object, _mockConfig.Object);

        var result = await handler.Handle(request, CancellationToken.None);
        
        result.Succeeded.ShouldBe(expect);

    }
}
