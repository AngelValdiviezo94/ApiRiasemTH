using EnrolApp.Application.Features.Clients.Commands.CreateCliente;
using FluentValidation.Results;
using Shouldly;
using Xunit;

namespace EnrolApp.Application.UnitTests.Features.Clients.Commands.CreateCliente;
public class CreateClienteCommandValidatorXUnitTests
{
    private readonly CreateClienteCommandValidator validator;

    public CreateClienteCommandValidatorXUnitTests()
    {
        validator = new CreateClienteCommandValidator();
    }

    [Theory]
    [InlineData("douglasborbor8@gmail.com","Coop. Sergio Toral","2022-05-20","M","0956852365", "37,42192", "-122,083954", "Douglas_2022@@", "C",true)] //Datos correctos ingresado 
    [InlineData("douglasborbor8.com", "Coop. Sergio Toral", "2022-05-20", "M", "0956325897", "569858", "595554", "Douglas_2022@@", "C", false)] //correo electronico incorrecto
    [InlineData("douglasborbor8@gmail.com", "Coop. Sergio Toral", "05-2022-20", "M", "0956235248", "569858", "595554", "Password", "C", false)] // formato de fecha incorrecta
    [InlineData("douglasborbor8@gmail.com", "Coop. Sergio Toral", "2022-05-20", "ML", "0956235248", "569858", "595554", "Password", "C", false)] // 2 caracteres en el genero
    [InlineData("douglasborbor8@gmail.com", "Coop. Sergio Toral", "2022-05-20", "M", "09562352", "569858", "595554", "Password", "C", false)] // cedula menor de 10
    [InlineData("douglasborbor8@gmail.com", "Coop. Sergio Toral", "2022-05-20", "M", "095685df98", "569858", "595554", "Password", "C", false)] // cedula alfanumerico
    [InlineData("douglasborbor8@gmail.com", "Coop. Sergio Toral", "2022-05-20", "M", "0951635825", "37.42192", "-122.083954", "Password", "H", false)] // Diferente el tipo de identificacion
    [InlineData("douglasborbor8@gmail.com", "Coop. Sergio Toral", "2022-05-20", "M", "0951635825", "37.42192", "-122.083954", "Password", "CP", false)] // Varios caracteres en tipo de identificacion
    public void CreateClienteCommandValidator_ReturnValidCreateCliente(string correo,string direccion,string fechaNacimiento,string genero,string identificacion,string latitud,string longitud,string password,string tipoIdentificacion,bool expected)
    {
        var obj = new CreateClienteRequest
        {
            Correo = correo,
            Direccion = direccion,
            FechaNacimiento = fechaNacimiento,
            Genero = genero,
            Identificacion = identificacion,
            Latitud = latitud,
            Longitud = longitud,
            Password = password,
            TipoIdentificacion = tipoIdentificacion
        };

        var model = new CreateClienteCommand(obj);

        ValidationResult result = validator.Validate(model);

        result.IsValid.ShouldBe(expected,result.ToString("===="));

    }


}
