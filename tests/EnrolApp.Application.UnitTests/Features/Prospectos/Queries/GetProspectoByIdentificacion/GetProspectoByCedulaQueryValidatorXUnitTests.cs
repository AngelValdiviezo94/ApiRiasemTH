using EnrolApp.Application.Features.Prospectos.Queries.GetProspectoByIdentificacion;
using FluentValidation.Results;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Prospectos.Queries.GetProspectoByIdentificacion;

public class GetProspectoByCedulaQueryValidatorXUnitTests
{
    private readonly GetProspectoByIdentificacionQueryValidator validator;

    public GetProspectoByCedulaQueryValidatorXUnitTests()
    {
        validator = new GetProspectoByIdentificacionQueryValidator();
    }
    [Theory]
    [InlineData("C", "C","0956329854",true)]
    [InlineData("F", "P", "FGSS454568", true)]
    [InlineData("C", "C", "094566332255", false)]
    public void GetProspectoByIdentificacionQueryValidator_ReturnTrueDataProspecto(string tipoProspecto, string tipoIdentificacion,string identificacion,bool expected)
    {
        var model = new GetProspectoByIdentificacionQuery(tipoProspecto, tipoIdentificacion, identificacion);

        ValidationResult result = validator.Validate(model);

        result.IsValid.ShouldBe(expected, result.ToString());
    }
}
