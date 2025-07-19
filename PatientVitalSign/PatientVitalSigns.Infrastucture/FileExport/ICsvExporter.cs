using PatientVitalSigns.Application;
using System.Collections.Generic;

namespace PatientVitalSigns.Infrastucture
    {
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<VitalSignsDto> eventExportDtos);
    }
}
