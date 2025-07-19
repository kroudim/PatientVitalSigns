
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using PatientVitalSigns.Domain;
using System.Linq;

namespace PatientVitalSigns.Application
  {
  public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, IEnumerable<PatientDto>>
    {
    private readonly IPatientRepository _patientRepository;

    public GetPatientsQueryHandler(IPatientRepository patientRepository)
      {
      _patientRepository = patientRepository;
      }

    public async Task<IEnumerable<PatientDto>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
      {
      var patients = await _patientRepository.GetAllAsync();
      return patients.Select(p => new PatientDto
        {
        Id = p.Id,
        Name = p.Name,
        SurName = p.SurName,
        Room = p.Room
        });
      }
    }
  }
