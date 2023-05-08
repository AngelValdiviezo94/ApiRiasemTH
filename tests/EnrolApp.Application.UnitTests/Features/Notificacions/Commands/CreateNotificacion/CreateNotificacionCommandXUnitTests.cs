using AutoFixture;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Notifications.Commands.CreateNotificacion;
using EnrolApp.Application.Features.Notifications.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Notificacion;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Notificacions.Commands.CreateNotificacion;

public class CreateNotificacionCommandXUnitTests
{
    private readonly Mock<IRepositoryAsync<BitacoraNotificacion>> _mockrepoNotifAsync;
    private readonly Mock<IRepositoryAsync<EventoDifusion>> _mockrepoEvDifAsync;
    private readonly Mock<IRepositoryAsync<Cliente>> _mockrepoCliAsync;

    public CreateNotificacionCommandXUnitTests()
    {                                                 
        _mockrepoNotifAsync = new Mock<IRepositoryAsync<BitacoraNotificacion>>();      
        _mockrepoEvDifAsync = new Mock<IRepositoryAsync<EventoDifusion>>();
        _mockrepoCliAsync = new Mock<IRepositoryAsync<Cliente>>();
    }

    //[Fact]
    //public async void CreateNotificacionCommand_ReturnTypeData()
    //{
    //    var fixture = new Fixture();
    //    fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    //    var evento = fixture.Build<List<EventoDifusion>>().Create();
    //    var cliente = fixture.Build<Cliente>().OmitAutoProperties().Create();
    //    var bitacora = fixture.Build<List<BitacoraNotificacion>>().OmitAutoProperties().Create();
    //    var request = fixture.Build<CreateNotificacionCommand>()
    //        .With(r => r.ReqNotif, fixture.Build<CreateNotificacionRequest>()
    //                              .With(x => x.CodigoEvento, "evento")
    //                              .Create())
    //        .Create();

    //    _mockrepoEvDifAsync.Setup(r => r.ListAsync(new EventoDifusionByCodigoSpec("evento"), CancellationToken.None)).ReturnsAsync(evento);
    //    _mockrepoCliAsync.Setup(r => r.FirstOrDefaultAsync(new ClienteByIdentificacionSpec("095456859852"), CancellationToken.None)).ReturnsAsync(cliente);
    //    _mockrepoNotifAsync.Setup(r => r.AddRangeAsync(bitacora, CancellationToken.None));


    //    var handler = new CreateNotificacionCommandHandler(_mockrepoNotifAsync.Object, _mockrepoEvDifAsync.Object, _mockrepoCliAsync.Object);

    //    var result = await handler.Handle(request, CancellationToken.None);

    //    result.Succeeded.ShouldBe(true);
    //}
}
