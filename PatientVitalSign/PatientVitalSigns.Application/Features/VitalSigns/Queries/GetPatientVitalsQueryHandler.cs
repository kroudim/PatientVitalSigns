
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using PatientVitalSigns.Domain;
using System.Linq;
using System.Collections;

namespace PatientVitalSigns.Application
  {
  public class GetPatientVitalsQueryHandler : IRequestHandler<GetPatientVitalsQuery, IEnumerable<VitalSignsDto>>
    {
    private readonly IPatientRepository _patientRepository;
    private readonly IVitalSignsRepository _vitalSignsRepository;

    public GetPatientVitalsQueryHandler(IPatientRepository patientRepository, IVitalSignsRepository vitalSignsRepository)
      {
      _patientRepository = patientRepository;
      _vitalSignsRepository = vitalSignsRepository;
      }

    public async Task<IEnumerable<VitalSignsDto>> Handle(GetPatientVitalsQuery request, CancellationToken cancellationToken)
      {
      var patient = await _patientRepository.GetByIdAsync(request.PatientId);
      if (patient == null)
        return null;

      var vitalsList = await _vitalSignsRepository.GetByPatientIdAsync(request.PatientId);
      var latest = vitalsList.OrderByDescending(v => v.TimeStamp).FirstOrDefault();

      if (latest == null)
        return null;

      return vitalsList.Select(x=> new VitalSignsDto
        {
        PatientId = patient.Id,
        Name = patient.Name,
        SurName = patient.SurName,
        Room = patient.Room,
        HeartRate = x.HeartRate.Value,
        BloodPressureSystolic = x.BloodPressure.Systolic ,
        BloodPressureDiastolic = x.BloodPressure.Diastolic,
        OxygenSaturation = x.OxygenSaturation.Value,
        TimeStamp = x.TimeStamp,
          });
      }
    }

  }
