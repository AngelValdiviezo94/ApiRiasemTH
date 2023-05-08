using FluentValidation;

namespace EnrolApp.Application.Features.Clients.Commands.CreateCliente;

public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
{
    public CreateClienteCommandValidator()
    {

        RuleFor(v => v.ClienteRequest.TipoIdentificacion)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
       .MaximumLength(1).WithMessage("{PropertyName} debe tener una longitud de {MaxLength} caracteres")
       .Custom((list,context) =>
       {
           if(list != "C" && list != "P" )
           {
               context.AddFailure("{PropertyName} solamente debe ser Cedula ó Pasaporte");
           }
       });

        When(v => v.ClienteRequest.TipoIdentificacion.ToUpper() == "C", () =>
        {
            RuleFor(v => v.ClienteRequest.Identificacion)
                .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
                .MinimumLength(10).WithMessage("{PropertyName} debe tener por lo menos {MinLength} caracteres")
                .Matches("^[0-9]+$").WithMessage("{PropertyName} debe contener ser solo numeros.");
        });

   
        When(v => v.ClienteRequest.TipoIdentificacion.ToUpper() == "P", () =>
        {
            RuleFor(v => v.ClienteRequest.Identificacion)
                .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(20).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
                .MinimumLength(5).WithMessage("{PropertyName} debe tener por lo menos {MinLength} caracteres");
        });


        RuleFor(v => v.ClienteRequest.FechaNacimiento)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
            .Matches(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))(\-)([0-2][0-9]|(3)[0-1])$").WithMessage("{PropertyName} formato incorrecto");

        RuleFor(v => v.ClienteRequest.Genero)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
            .MaximumLength(1).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
            .MinimumLength(1).WithMessage("{PropertyName} no debe ser menor de {MaxLength} caracteres");


        RuleFor(v => v.ClienteRequest.Direccion)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
            .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

        RuleFor(v => v.ClienteRequest.Latitud)
          .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
          .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


        RuleFor(v => v.ClienteRequest.Correo)
         .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
         .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
         .EmailAddress().WithMessage("{PropertyName} debe ser una direcciòn de email valida")
         .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

        RuleFor(v => v.ClienteRequest.TipoCliente)
         .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
         .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


    }
}