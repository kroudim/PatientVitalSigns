
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using PatientVitalSigns.Domain;
using System.Linq;

namespace PatientVitalSigns.Application
  {
  public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, PatientDto>
    {
    private readonly IPatientRepository _patientRepository;

    public GetPatientQueryHandler(IPatientRepository patientRepository)
      {
      _patientRepository = patientRepository;
      }

    public async Task<PatientDto> Handle(GetPatientQuery request, CancellationToken cancellationToken)
      {
      var patient = await _patientRepository.GetByIdAsync(request.PatientId);
      return new PatientDto
        {
        Id = patient.Id,
        Name = patient.Name,
        SurName = patient.SurName,
        Room = patient.Room
        };
      }
    }
  }
