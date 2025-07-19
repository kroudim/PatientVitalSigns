
using PatientVitalSigns.Domain;
using System;

namespace PatientVitalSigns.Application
    {
    public class VitalSignsDto
        {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Room { get; set; }
        public int HeartRate { get; set; }
        public int BloodPressureSystolic { get; set; }
        public int BloodPressureDiastolic { get; set; }
        public decimal OxygenSaturation { get; set; }
        public DateTime TimeStamp { get; set; }

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

        public VitalSignStatus OxygenSaturationStatus {
            get {
                if (HeartRate < 90m) return VitalSignStatus.Critical;
                if (HeartRate <= 95m && HeartRate > 90) return VitalSignStatus.Warning;
                return VitalSignStatus.Normal;
                }

            }
        }
    }
