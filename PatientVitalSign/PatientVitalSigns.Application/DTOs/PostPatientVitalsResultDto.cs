
using PatientVitalSigns.Domain;
using System;

namespace PatientVitalSigns.Application
  {
  public class PostPatientVitalsResultDto
    {
    public bool Success { get; set; }
    public int PatientId { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime? SavedAt { get; set; }



        }
  }
