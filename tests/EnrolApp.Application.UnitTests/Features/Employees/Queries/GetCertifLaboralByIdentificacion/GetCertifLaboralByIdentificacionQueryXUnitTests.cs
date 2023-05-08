using AutoFixture;
using AutoMapper;
using EnrolApp.Application.Common.Mappings;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Employees.Dto;
using EnrolApp.Application.Features.Employees.Interfaces;
using EnrolApp.Application.Features.Employees.Queries.GetCertifLaboralByIdentificacion;
using EnrolApp.Domain.Entities.Nomina;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace EnrolApp.Application.UnitTests.Features.Employees.Queries.GetCertifLaboralByIdentificacion
{
    public class GetCertifLaboralByIdentificacionQueryXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IReportesEmpleado> _mockrepository;
        public GetCertifLaboralByIdentificacionQueryXUnitTests()
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
        //public async void GetCertificadoLaboralByIdentificacionAsync_ReturrnDataCertificadoLaboral(string identificacion)
        //{
        //    var fixture = new Fixture();
        //    var certificado = fixture.Create<CertificadoLaboral>();
        //    _mockrepository.Setup(r => r.GetCertificadoLaboralByIdentificacionAsync("0951635390")).ReturnsAsync(certificado);

        //    var handler = new GetCertifLaboralByIdentificacionQueryHandler(_mockrepository.Object, _mapper);

        //    var request = new GetCertifLaboralByIdentificacionQuery(identificacion);

        //    var result = await handler.Handle(request, CancellationToken.None);

        //    result.ShouldBeOfType<ResponseType<CertificadoLaboralType>>();
        //}

    }
}
