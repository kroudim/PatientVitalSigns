
using PatientVitalSigns.Domain;

namespace PatientVitalSigns.Application
  {
  public class PostPatientVitalsDto
    {
    public int HeartRate { get; set; }
    public int BloodPressureSystolic { get; set; }
    public int BloodPressureDiastolic { get; set; }
    public decimal OxygenSaturation { get; set; }
        public VitalSignStatus HeartRateStatus
            {
            get
                {
                if (HeartRate > 120) return VitalSignStatus.Critical;
                if (HeartRate > 100 && HeartRate <= 120) return VitalSignStatus.Warning;
                if (HeartRate > 60 && HeartRate <= 100) return VitalSignStatus.Normal;
                return VitalSignStatus.Warning;
                }
            }
        public VitalSignStatus BloodPressureStatus
            {
            get
                {
                // Critical: High Hypertension or Hypotension
                if (BloodPressureSystolic > 139 || BloodPressureDiastolic > 90)
                    return VitalSignStatus.Critical;
                // Warning: Pre-hypertension
                if ((BloodPressureSystolic >= 120 && BloodPressureSystolic <= 139) ||
                    (BloodPressureDiastolic >= 80 && BloodPressureDiastolic <= 89))
                    return VitalSignStatus.Warning;
                // Normal
                if ((BloodPressureSystolic >= 90 && BloodPressureSystolic <= 119) &&
                    (BloodPressureDiastolic >= 60 && BloodPressureDiastolic <= 79))
                    return VitalSignStatus.Normal;
                // Anything else (shouldn't occur, but for safety)
                return VitalSignStatus.Warning;
                }
            }

        public VitalSignStatus OxygenSaturationStatus
            {
            get
                {
                if (OxygenSaturation < 90m) return VitalSignStatus.Critical;
                if (OxygenSaturation < 95m && OxygenSaturation >= 90) return VitalSignStatus.Warning;
                return VitalSignStatus.Normal;
                }

            }
        }
  }
