using FluentValidation;

namespace EnrolApp.Application.Features.Notifications.Commands.CreateNotificacion;

public class CreateNotificacionCommandValidator : AbstractValidator<CreateNotificacionCommand>
{
    public CreateNotificacionCommandValidator()
    {

        RuleFor(v => v.ReqNotif.Identificacion)
                 .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
                 .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                 .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
                 .MinimumLength(10).WithMessage("{PropertyName} debe tener por lo menos {MinLength} caracteres")
                 .Matches("^[0-9]+$").WithMessage("{PropertyName} debe contener ser solo numeros.");

        RuleFor(v => v.ReqNotif.CodigoEvento)
          .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
          .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

    }
}