using EnrolApp.Application.Features.Clients.Commands.ActivateCliente;
using FluentValidation.Results;
using Shouldly;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Clients.Commands.ActivateCliente;
public class ActivateClienteCommandValidatorXUnitTests
{
    private readonly ActivateClienteCommandValidator validator;

    public ActivateClienteCommandValidatorXUnitTests()
    {
        validator = new ActivateClienteCommandValidator();
    }
    [Theory]
    [InlineData(null,false)]
    [InlineData("", false)]
    [InlineData("598563265874596523659854", false)] //mayor a 20 caracteres
    [InlineData("59856326587459652365", true)] //20 caracteres
    [InlineData("DSND154", false)] // menor de 10 caracteres
    [InlineData("0954df5265", false)] //si es 10 solo debe contener numeros
    [InlineData("0956853698", true)] //si es 10 solo debe contener numeros
    [InlineData("GKLOUD098565982569", true)] //si es mayor de 10 alfanumerico
    public void ActivateClienteCommandValidator_ValidaNumeroIdentificacion(string identificacion,bool expected)
    {
        var model = new ActivateClienteCommand(identificacion);

        ValidationResult result = validator.Validate(model);

        result.IsValid.ShouldBe(expected,result.ToString("==="));
    }
}
