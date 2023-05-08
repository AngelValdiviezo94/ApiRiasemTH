using EnrolApp.Application.Features.Employees.Queries.GetInfoGeneralByIdentificacion;
using FluentValidation.Results;
using Shouldly;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Employees.Queries.GetInfoGeneralByIdentificacion;

public class GetInfoGeneralByIdentificacionQueryValidatorXUnitTests
{
    private readonly GetInfoGeneralByIdentificacionQueryValidator validator;

    public GetInfoGeneralByIdentificacionQueryValidatorXUnitTests()
    {
        validator = new GetInfoGeneralByIdentificacionQueryValidator();
    }
    [Theory]
    [InlineData("", false)]
    [InlineData("0956982ddd", false)]
    [InlineData("0956982536", true)]
    //[InlineData("526987568545", false)]
    public void GetInfoGeneralByIdentificacionQueryValidator_ReturnTrueIdentificacion(string identificacion,bool expected)
    {
        var model = new GetInfoGeneralByIdentificacionQuery(identificacion);

        ValidationResult result = validator.Validate(model);

        result.IsValid.ShouldBe(expected, result.ToString());
    }

}
