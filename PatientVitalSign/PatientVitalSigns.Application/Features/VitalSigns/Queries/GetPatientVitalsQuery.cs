
using System.Collections.Generic;
using MediatR;

namespace PatientVitalSigns.Application
  {
  public class GetPatientVitalsQuery : IRequest<IEnumerable<VitalSignsDto>>
    {
    public int PatientId { get; }

    public GetPatientVitalsQuery(int patientId)
      {
      PatientId = patientId;
      }
    }
  }
