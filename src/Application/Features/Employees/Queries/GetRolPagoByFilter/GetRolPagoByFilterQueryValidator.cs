using FluentValidation;


namespace EnrolApp.Application.Features.Employees.Queries.GetRolPagoByFilter;

public class GetRolPagoByFilterQueryValidator : AbstractValidator<GetRolPagoByFilterQuery>
{
    public GetRolPagoByFilterQueryValidator()
    {

        RuleFor(v => v.Identificacion)
        .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
        .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
   
        When(v => v.Identificacion.Length == 10, () =>
        {
            RuleFor(v => v.Identificacion)
                .Matches("^[0-9]+$").WithMessage("{PropertyName} debe contener ser solo numeros.");
        });

        RuleFor(v => v.FechaCorte)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


    }
}

