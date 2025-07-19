
using System.Collections.Generic;
using MediatR;

namespace PatientVitalSigns.Application
  {
  public class CreatePatientVitalsCommand : IRequest<PostPatientVitalsResultDto>
    {
    public int PatientId { get; }
    public PostPatientVitalsDto Vitals { get; }

    public CreatePatientVitalsCommand(int patientId, PostPatientVitalsDto vitals)
      {
      PatientId = patientId;
      Vitals = vitals;
      }
    }
  }
