
namespace PatientVitalSigns.Domain
{
  public class BloodPressure
    {
    public int Systolic { get; set; }
    public int Diastolic { get; set; }

    public BloodPressure(int systolic, int diastolic)
      {
      Systolic = systolic;
      Diastolic = diastolic;
      }
    }
  }
