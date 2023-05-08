using FluentValidation;


namespace EnrolApp.Application.Features.Notifications.Queries.GetProspectoByIdentificacion;

public class GetNotificacionByIdentificacionQueryValidator : AbstractValidator<GetNotificacionByIdentificacionQuery>
{
    public GetNotificacionByIdentificacionQueryValidator()
    {

        RuleFor(v => v.Identificacion)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

    }
}
