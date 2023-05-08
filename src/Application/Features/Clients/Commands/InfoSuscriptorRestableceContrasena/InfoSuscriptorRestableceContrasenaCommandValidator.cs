using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Clients.Commands.InfoSuscriptorRestableceContrasena
{
    public class InfoSuscriptorRestableceContrasenaCommandValidator : AbstractValidator<InfoSuscriptorRestableceContrasenaCommand>
    {
        public InfoSuscriptorRestableceContrasenaCommandValidator()
        {
            RuleFor(v => v.Identificacion)
                .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(20).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
                .MinimumLength(10).WithMessage("{PropertyName} no debe ser menor de {MaxLength} caracteres")
                .Custom((list, context) =>
                {
                    if (!string.IsNullOrEmpty(list))
                    {
                        if (list.Length == 10 && Regex.IsMatch(list, @"^[a-z0-9A-Z]+$") && !Regex.IsMatch(list, @"^[0-9]+$"))
                        {
                            context.AddFailure("No debe ser alfanumerica si solamente tiene 10 caracteres");
                        }
                    }

                });
        }
    }
}