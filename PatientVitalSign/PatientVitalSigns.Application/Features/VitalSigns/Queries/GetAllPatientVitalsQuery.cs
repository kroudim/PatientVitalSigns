
using System.Collections.Generic;
using MediatR;

namespace PatientVitalSigns.Application
  {
  public class GetAllPatientVitalsQuery : IRequest<IEnumerable<VitalSignsDto>>
    {
    }
  }
