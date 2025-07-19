
using System.Collections.Generic;
using MediatR;

namespace PatientVitalSigns.Application
  {
  public class GetPatientsQuery : IRequest<IEnumerable<PatientDto>>
    {
    }
  }
