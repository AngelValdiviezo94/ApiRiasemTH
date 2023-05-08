using EnrolApp.Application.Features.Horarios.Queries.GetHorarioByFilter;
using FluentValidation.Results;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Horarios.Queries.GetHorarioByFilter;

public class GetHorarioByFilterQueryValidatorXUnitTests
{
    private readonly GetHorarioByFilterQueryValidator validator;

    public GetHorarioByFilterQueryValidatorXUnitTests()
    {
        validator = new GetHorarioByFilterQueryValidator();
    }

    [Theory]
    [InlineData("","","", false)]
    [InlineData("0956352859","2022-09-05","2022-09-09", true)]
    [InlineData("095425fgrt", "2022-09-05", "2022-09-09", false)]
    [InlineData("0945874512", "2022-09", "2022-09-09", false)]//fecha desde incorrecta
    [InlineData("0945874512", "2022-09", "2022-09", false)]//fecha hasta incorrecta

    public void GetCupoCreditoByIdentificacionQueryValidator_ReturnTrueIdentificacion(string identificacion, string desde,string hasta, bool expected)
    {
        //var model = new GetHorarioByFilterQuery(identificacion,desde,hasta);

        //ValidationResult result = validator.Validate(model);

        //result.IsValid.ShouldBe(expected, result.ToString());
    }
}