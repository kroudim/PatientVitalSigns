
using System;

namespace PatientVitalSigns.Domain
  {
  public class VitalSigns
    {
    public int Id { get; set; }
    public int PatientId { get; set; }
    public HeartRate HeartRate { get; set; }
    public BloodPressure BloodPressure { get; set; }
    public OxygenSaturation OxygenSaturation { get; set; }
    public DateTime TimeStamp { get; set; }        
    }
  }
