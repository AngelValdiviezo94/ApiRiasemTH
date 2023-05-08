using AutoFixture;
using EnrolApp.Application.Features.Notifications.Commands.CreateNotificacion;
using FluentValidation.Results;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Notificacions.Commands.CreateNotificacion;

public class CreateNotificacionCommandValidatorXUnitTests
{
    private readonly CreateNotificacionCommandValidator validator;

    public CreateNotificacionCommandValidatorXUnitTests()
    {
        validator = new CreateNotificacionCommandValidator();
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("598563265874596523659854", false)] //mayor a 20 caracteres
    [InlineData("59856326587459652365", true)] //20 caracteres
    [InlineData("DSND154", false)] // menor de 10 caracteres
    [InlineData("0954df5265", false)] //si es 10 solo debe contener numeros
    [InlineData("0956853698", true)] //si es 10 solo debe contener numeros
    [InlineData("GKLOUD098565982569", true)] //si es mayor de 10 alfanumerico
    public void CreateNotificacionCommandValidator_ValidaNumeroIdentificacion(string identificacion, bool expected)
    {
        var fixture = new Fixture();
        var obj = fixture.Build<CreateNotificacionRequest>()
            .With(r => r.Identificacion,identificacion)
            .Create();
        var model = new CreateNotificacionCommand(obj);

        ValidationResult result = validator.Validate(model);

        result.IsValid.ShouldBe(expected, result.ToString("==="));
    }
}


