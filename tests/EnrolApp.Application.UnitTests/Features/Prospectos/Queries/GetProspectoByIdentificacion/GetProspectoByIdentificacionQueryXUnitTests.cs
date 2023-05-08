using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using AutoFixture;
using Moq;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Domain.Entities.Suscripcion;
using EnrolApp.Domain.Entities.Common;
using Microsoft.Extensions.Configuration;

using EnrolApp.Application.Features.Prospectos.Queries.GetProspectoByIdentificacion;
using EnrolApp.Application.Features.Prospectos.Specifications;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Prospectos.Dto;

namespace EnrolApp.Application.UnitTests.Features.Prospectos.Queries.GetProspectoByIdentificacion;

public class GetProspectoByIdentificacionQueryXUnitTests
{
    private readonly Mock<IRepositoryAsync<Prospecto>> _mockrepositoryAsync;
    private readonly Mock<IRepositoryAsync<Cliente>> _mockrepositoryClAsync;
    private readonly Mock<IApisConsumoAsync> _mockrepositoryApis;
    private readonly Mock<IConfiguration> _mockconfig;

    public GetProspectoByIdentificacionQueryXUnitTests()
    {
        _mockrepositoryAsync = new Mock<IRepositoryAsync<Prospecto>>();
        _mockrepositoryClAsync = new Mock<IRepositoryAsync<Cliente>>();
        _mockrepositoryApis = new Mock<IApisConsumoAsync>();
        _mockconfig = new Mock<IConfiguration>();
    }
    //[Theory]
    //[InlineData(true,true,false,false)]
    //[InlineData(true, false, false, false)]
    //[InlineData(false, false, false, false)]
    //[InlineData(false, false, true, false)]
    //[InlineData(false, false, true, true)]
    //public void GetProspectoByIdentificacionQueryHandler_RetornaTipoDato(bool isCliente,bool servicioActivo,bool isProspecto,bool success)
    //{
    //    var fixture = new Fixture();
    //    fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    //    var cliente = fixture.Build<Cliente>()
    //                    .With(r => r.Identificacion, "0951635390")
    //                    .With(r => r.TipoIdentificacion, "C")
    //                    .With(r => r.ServicioActivo, servicioActivo)
    //                    .OmitAutoProperties()
    //                    .Create();
    //    var request = fixture.Build<GetProspectoByIdentificacionQuery>()
    //                    .With(r => r.Identificacion,"0980132010")
    //                    .With(r => r.TipoIdentificacion,"C").Create();
    //    var prospecto = fixture.Create<Prospecto>();
    //    var endPoint = fixture.Build<(bool Success, object Data)>()
    //                .With(r => r.Success, true)
    //                .Create();
    //    var resultEndPoint = fixture.Build<(bool Success, ResponseType<string> obj)>()
    //                .With(r => r.Success, true)
    //                .With(r => r.obj, fixture.Build<ResponseType<string>>()
    //                                         .With(x => x.StatusCode, "000")
    //                                         .With(x => x.Succeeded, success)
    //                                         .Create())
    //                .Create();


    //    var mockConfigurationSection = new Mock<IConfigurationSection>();
    //    mockConfigurationSection.Setup(r => r.Value).Returns("0.15");
    //    _mockconfig.Setup(con => con.GetSection("ConsumoApis:UrlBaseApiUtils")).Returns(mockConfigurationSection.Object);
    //    _mockconfig.Setup(con => con.GetSection("ConsumoApis:UrlBaseApiAuth")).Returns(mockConfigurationSection.Object);
    //    _mockconfig.Setup(con => con.GetSection("EndPointConsumoApis:ApiAuth:ValidarUsuarioLdap")).Returns(mockConfigurationSection.Object);
    //    _mockconfig.Setup(con => con.GetSection("EndPointConsumoApis:ApiAuth:GenerarOtp")).Returns(mockConfigurationSection.Object);
    //    _mockconfig.Setup(con => con.GetSection("Notificaciones:PlantillaOtp")).Returns(mockConfigurationSection.Object);
    //    _mockconfig.Setup(con => con.GetSection("Notificaciones:AsuntoCodigoOtp")).Returns(mockConfigurationSection.Object);
    //    _mockconfig.Setup(con => con.GetSection("EndPointConsumoApis:ApiUtils:EnviarCorreo")).Returns(mockConfigurationSection.Object);
    //    _mockconfig.Setup(con => con.GetSection("EndPointConsumoApis:ApiAuth:CrearUsuarioLdap")).Returns(mockConfigurationSection.Object);
    //    if (isCliente)
    //    {
    //        _mockrepositoryClAsync.Setup(r => r.FirstOrDefaultAsync(It.IsAny<ClienteByIdentificacionSpec>(), CancellationToken.None)).ReturnsAsync(cliente);
    //    }
    //    else
    //    {
    //        _mockrepositoryClAsync.Setup(r => r.FirstOrDefaultAsync(new ClienteByIdentificacionSpec("C", "0951635390"), CancellationToken.None)).ReturnsAsync(cliente);
    //    }
    //    if (isProspecto)
    //    {
    //        _mockrepositoryAsync.Setup(r => r.FirstOrDefaultAsync(It.IsAny<ProspectoByIdentificacionSpec>(), CancellationToken.None)).ReturnsAsync(prospecto);
    //    }
    //    else
    //    {
    //        _mockrepositoryAsync.Setup(r => r.FirstOrDefaultAsync(new ProspectoByIdentificacionSpec("0951635390", "C"), CancellationToken.None)).ReturnsAsync(prospecto);
    //    }
    //    _mockrepositoryApis.Setup(r => r.PostEndPoint(It.IsAny<object>(), "0.150.15", "0.15")).ReturnsAsync(resultEndPoint);

    //    var handler = new GetProspectoByIdentificacionQueryHandler(_mockrepositoryAsync.Object, _mockrepositoryClAsync.Object, _mockrepositoryApis.Object, _mockconfig.Object);

    //    var result = handler.Handle(request, CancellationToken.None);


    //    result.ShouldBeOfType<Task<ResponseType<ProspectoType>>>();

        
    //}
}
