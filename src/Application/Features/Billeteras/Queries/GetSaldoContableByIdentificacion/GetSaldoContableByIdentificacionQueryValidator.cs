using FluentValidation;

namespace EnrolApp.Application.Features.Billeteras.Queries.GetSaldoContableByIdentificacion;

public class GetSaldoContableByIdentificacionQueryValidator : AbstractValidator<GetSaldoContableByIdentificacionQuery>
{
    public GetSaldoContableByIdentificacionQueryValidator()
    {
        RuleFor(v => v.Identificacion)
        .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
        .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
        .MinimumLength(10).WithMessage("{PropertyName} debe tener por lo menos {MinLength} caracteres");

        When(v => v.Identificacion.Length == 10, () =>
        {
            RuleFor(v => v.Identificacion)
                .Matches("^[0-9]+$").WithMessage("{PropertyName} debe contener ser solo numeros.");
        });

    }
}