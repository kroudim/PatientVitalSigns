
namespace PatientVitalSigns.Domain
  {
  public class OxygenSaturation
    {
    public decimal Value { get; set; }

    public OxygenSaturation(decimal value)
      {
      Value = value;
      }
    }
  }
