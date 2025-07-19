
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using PatientVitalSigns.Domain;

namespace PatientVitalSigns.Application
  {
  public class CreatePatientVitalsCommandHandler : IRequestHandler<CreatePatientVitalsCommand, PostPatientVitalsResultDto>
    {
    private readonly IPatientRepository _patientRepository;
    private readonly IVitalSignsRepository _vitalSignsRepository;

    public CreatePatientVitalsCommandHandler(IPatientRepository patientRepository, IVitalSignsRepository vitalSignsRepository)
      {
      _patientRepository = patientRepository;
      _vitalSignsRepository = vitalSignsRepository;
      }

    public async Task<PostPatientVitalsResultDto> Handle(CreatePatientVitalsCommand request, CancellationToken cancellationToken)
      {
      // Check if patient exists
      var patient = await _patientRepository.GetByIdAsync(request.PatientId);
      if (patient == null)
        {
        return new PostPatientVitalsResultDto
          {
          Success = false,
          PatientId = request.PatientId,
          ErrorMessage = "Patient not found",
          SavedAt = null
          };
        }

      var vitals = new VitalSigns
        {
        PatientId = request.PatientId,
        HeartRate = new HeartRate(request.Vitals.HeartRate),
        BloodPressure = new BloodPressure(request.Vitals.BloodPressureSystolic,request.Vitals.BloodPressureDiastolic),
        OxygenSaturation = new OxygenSaturation(request.Vitals.OxygenSaturation),
        TimeStamp = DateTime.UtcNow
        };

      await _vitalSignsRepository.AddAsync(vitals);

      return new PostPatientVitalsResultDto
        {
        Success = true,
        PatientId = request.PatientId,
        ErrorMessage = null,
        SavedAt = vitals.TimeStamp
          };
      }
    }
  }
