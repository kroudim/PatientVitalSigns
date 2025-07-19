using PatientVitalSigns.Application;

namespace PatientVitalSignsSolution.WebApp.Models
{
    internal class PatientMonitorViewModel
    {
        public PatientDto Patient { get; set; }
        public List<VitalSignsDto> VitalSigns { get; set; }
    }
}