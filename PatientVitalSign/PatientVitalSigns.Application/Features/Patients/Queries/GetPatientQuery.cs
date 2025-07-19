
using System.Collections.Generic;
using MediatR;

namespace PatientVitalSigns.Application
  {
  public class GetPatientQuery : IRequest<PatientDto>
    {
        public int PatientId { get; }

        public GetPatientQuery(int patientId)
            {
            PatientId = patientId;
            }
        }
  }
