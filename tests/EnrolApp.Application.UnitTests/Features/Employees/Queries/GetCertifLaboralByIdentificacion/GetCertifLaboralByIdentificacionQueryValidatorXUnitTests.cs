using EnrolApp.Application.Features.Employees.Queries.GetCertifLaboralByIdentificacion;
using FluentValidation.Results;
using Shouldly;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Employees.Queries.GetCertifLaboralByIdentificacion;

public class GetCertifLaboralByIdentificacionQueryValidatorXUnitTests
{
    private readonly GetCertifLaboralByIdentificacionQueryValidator validator;

    public GetCertifLaboralByIdentificacionQueryValidatorXUnitTests()
    {
        validator = new GetCertifLaboralByIdentificacionQueryValidator();
    }

    [Theory]
    [InlineData("",false)]
    [InlineData("0956982ddd", false)]
    [InlineData("0956982536",true)]
    //[InlineData("526987568545", false)]
    public void GetCertifLaboralByIdentificacionQueryValidator_ReturnTrueIdentificacion(string identificacion,bool expected)
    {
        var model = new GetCertifLaboralByIdentificacionQuery(identificacion);

        ValidationResult result = validator.Validate(model);

        result.IsValid.ShouldBe(expected, result.ToString());
    }
}
