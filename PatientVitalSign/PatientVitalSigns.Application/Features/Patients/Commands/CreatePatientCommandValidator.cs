using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PatientVitalSigns.Domain;
using FluentValidation;

namespace PatientVitalSigns.Application
    {
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
        {
        public CreatePatientCommandValidator()
            {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.SurName)
                .NotEmpty().WithMessage("SurName is required.")
                .MaximumLength(100).WithMessage("SurName must not exceed 100 characters.");

            RuleFor(x => x.Room)
                    .GreaterThan(0).WithMessage("Room must be a positive integer.");
            }
        }
    }