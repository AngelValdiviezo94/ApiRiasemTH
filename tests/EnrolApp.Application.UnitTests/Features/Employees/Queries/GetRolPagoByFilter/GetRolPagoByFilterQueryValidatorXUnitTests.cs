using EnrolApp.Application.Features.Employees.Queries.GetRolPagoByFilter;
using FluentValidation.Results;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Employees.Queries.GetRolPagoByFilter;

public class GetRolPagoByFilterQueryValidatorXUnitTests
{
    private readonly GetRolPagoByFilterQueryValidator validator;

    public GetRolPagoByFilterQueryValidatorXUnitTests()
    {
        validator = new GetRolPagoByFilterQueryValidator();
    }

    [Theory]
    [InlineData("", "",false)]
    [InlineData("0956982ddd", "2022-05-15", false)]
    [InlineData("0956982536", "2022-05-15", false)]
    [InlineData("0956982536", "2022-05", true)]
    //[InlineData("526987568545","2022-05-15", false)]
    public void GetCertifLaboralByIdentificacionQueryValidator_ReturnTrueIdentificacion(string identificacion, string fechaCorte ,bool expected)
    {
        var model = new GetRolPagoByFilterQuery(identificacion,fechaCorte);

        ValidationResult result = validator.Validate(model);

        result.IsValid.ShouldBe(expected, result.ToString());
    }

}
