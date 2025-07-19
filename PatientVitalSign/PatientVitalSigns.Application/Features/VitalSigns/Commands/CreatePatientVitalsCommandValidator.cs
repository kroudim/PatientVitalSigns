
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using PatientVitalSigns.Domain;
using FluentValidation;

namespace PatientVitalSigns.Application
  {
    public class CreatePatientVitalsCommandValidator : AbstractValidator<CreatePatientVitalsCommand>
        {
        public CreatePatientVitalsCommandValidator()
            {
            RuleFor(x => x.PatientId)
                .GreaterThan(0).WithMessage("PatientId must be a positive integer.");

            RuleFor(x => x.Vitals)
                .NotNull().WithMessage("Vitals data is required.");

            When(x => x.Vitals != null, () =>
            {
                RuleFor(x => x.Vitals.HeartRate)
                    .InclusiveBetween(30, 220).WithMessage("Heart rate must be between 30 and 220 bpm.");

                RuleFor(x => x.Vitals.BloodPressureSystolic)
                    .InclusiveBetween(50, 250).WithMessage("Systolic blood pressure must be between 50 and 250.");

                RuleFor(x => x.Vitals.BloodPressureDiastolic)
                    .InclusiveBetween(30, 150).WithMessage("Diastolic blood pressure must be between 30 and 150.");

                RuleFor(x => x.Vitals.OxygenSaturation)
                    .InclusiveBetween(50, 100).WithMessage("Oxygen saturation must be between 50 and 100.");
            });
            }
        }
    }
