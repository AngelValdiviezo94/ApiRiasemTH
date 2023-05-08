using EnrolApp.Application.Features.Horarios.Queries.GetHorarioByFilter;
using FluentValidation;


namespace EnrolApp.Application.Features.Horarios.Queries.GetHorarioByFilter;

public class GetHorarioByFilterQueryValidator : AbstractValidator<GetHorarioByFilterQuery>
{
    public GetHorarioByFilterQueryValidator()
    {

        RuleFor(v => v.Identificacion)
        .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
        .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
   
        When(v => v.Identificacion.Length == 10, () =>
        {
            RuleFor(v => v.Identificacion)
                .Matches("^[0-9]+$").WithMessage("{PropertyName} debe contener ser solo numeros.");
        });

        RuleFor(v => v.FechaDesde)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.FechaHasta)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


    }
}

