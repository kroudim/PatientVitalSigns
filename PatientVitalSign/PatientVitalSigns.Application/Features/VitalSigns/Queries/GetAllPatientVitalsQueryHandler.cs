
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using PatientVitalSigns.Domain;
using System.Linq;
using System.Collections;
using System.Net.Http;

namespace PatientVitalSigns.Application
  {
  public class GetAllPatientVitalsQueryHandler : IRequestHandler<GetAllPatientVitalsQuery, IEnumerable<VitalSignsDto>>
    {
    private readonly IPatientRepository _patientRepository;
    private readonly IVitalSignsRepository _vitalSignsRepository;

    public GetAllPatientVitalsQueryHandler(IPatientRepository patientRepository, IVitalSignsRepository vitalSignsRepository)
      {
      _patientRepository = patientRepository;
      _vitalSignsRepository = vitalSignsRepository;
      }

    public async Task<IEnumerable<VitalSignsDto>> Handle(GetAllPatientVitalsQuery request, CancellationToken cancellationToken)
      {
      var patients = await _patientRepository.GetAllAsync();
      if (patients == null)
        return null;

            var vitalSigns = new List<VitalSigns>();

            foreach (var patientid in patients.Select(x => x.Id))
                {
                var vitalsList = await _vitalSignsRepository.GetByPatientIdAsync(patientid);
                if (vitalsList != null)
                    vitalSigns.AddRange(vitalsList);
                }

            return vitalSigns.Select(x=> new VitalSignsDto
        {
        PatientId = x.Id,
        Name = patients.FirstOrDefault(y=>x.PatientId == x.PatientId).Name ,
        SurName = patients.FirstOrDefault(y => x.PatientId == x.PatientId).SurName,
        Room = patients.FirstOrDefault(y => x.PatientId == x.PatientId).Room,
        HeartRate = x.HeartRate.Value,
        BloodPressureSystolic = x.BloodPressure.Systolic ,
        BloodPressureDiastolic = x.BloodPressure.Diastolic,
        OxygenSaturation = x.OxygenSaturation.Value,
        TimeStamp = x.TimeStamp,
          });
      }
    }

  }
