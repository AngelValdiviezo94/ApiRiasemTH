using FluentValidation;


namespace EnrolApp.Application.Features.Prospectos.Queries.GetProspectoByIdentificacion;

public class GetProspectoByIdentificacionQueryValidator : AbstractValidator<GetProspectoByIdentificacionQuery>
{
    public GetProspectoByIdentificacionQueryValidator()
    {

        RuleFor(v => v.TipoIdentificacion)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
       .MaximumLength(1).WithMessage("{PropertyName} debe tener una longitud de {MaxLength} caracteres");

        When(v => v.TipoIdentificacion.ToUpper() == "C", () =>
        {
            RuleFor(v => v.Identificacion)
                .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
                .MinimumLength(10).WithMessage("{PropertyName} debe tener por lo menos {MinLength} caracteres")
                .Matches("^[0-9]+$").WithMessage("{PropertyName} debe contener ser solo numeros.");
        });


        When(v => v.TipoIdentificacion.ToUpper() == "P", () =>
        {
            RuleFor(v => v.Identificacion)
                .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(20).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
                .MinimumLength(5).WithMessage("{PropertyName} debe tener por lo menos {MinLength} caracteres");
        });


    }
}
