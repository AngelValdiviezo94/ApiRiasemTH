using AutoFixture;
using AutoMapper;
using EnrolApp.Application.Common.Mappings;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Horarios.Dto;
using EnrolApp.Application.Features.Horarios.Interfaces;
using EnrolApp.Application.Features.Horarios.Queries.GetHorarioByFilter;
using EnrolApp.Domain.Entities.Horario;
using Moq;
using Shouldly;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Horarios.Queries.GetHorarioByFilter;




public class GetHorarioByFilterQueryXUnitTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IHorario> _mockrepository;
    public GetHorarioByFilterQueryXUnitTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockrepository = new Mock<IHorario>();

    }

    //[Theory]
    //[InlineData("0951635390", "2022-05-15", "2022-05-15")]
    ////[InlineData("0951635390", "2022-05-15","2022-05-18",false)]
    //public async void GetHorarioByFilterAsync_ReturnTypeData(string identificacion, string fechaDesde, string fechaHasta)
    //{
    //    var fixture = new Fixture();
    //    var horario = fixture.Create<List<Horario>>();

    //    _mockrepository.Setup(r => r.GetHorarioByFilterAsync("0951635390", "2022-05-15", "2022-05-15")).ReturnsAsync(horario);

    //    var handler = new GetHorarioByFilterQueryHandler(_mockrepository.Object, _mapper);

    //    var request = new GetHorarioByFilterQuery(identificacion, fechaDesde, fechaHasta);
         
    //    var result = await handler.Handle(request, CancellationToken.None);

    //    result.ShouldBeOfType<ResponseType<List<HorarioType>>>();
        
    //}
}
