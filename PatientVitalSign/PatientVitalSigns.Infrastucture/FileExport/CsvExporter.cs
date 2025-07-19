using PatientVitalSigns.Application;
using System.Text;

namespace PatientVitalSigns.Infrastucture
    {
    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportEventsToCsv(List<VitalSignsDto> eventExportDtos)
        {

            var csv = new StringBuilder();
            //// Header
            csv.AppendLine("PatientId,Name,SurName,Room,HeartRate,BloodPressureSystolic,BloodPressureDiastolic,OxygenSaturation,TimeStamp");

            foreach (var vitals in eventExportDtos.OrderBy(v => v.TimeStamp))
                {
                csv.AppendLine($"{vitals.PatientId}," +
                               $"\"{vitals.Name}\"," +
                               $"\"{vitals.SurName}\"," +
                               $"{vitals.Room}," +
                               $"{vitals.HeartRate}," +
                               $"{vitals.BloodPressureSystolic}," +
                               $"{vitals.BloodPressureDiastolic}," +
                               $"{vitals.OxygenSaturation}," +
                               $"{vitals.TimeStamp:yyyy-MM-dd HH:mm:ss}");
                }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return bytes;

           // return File(bytes, "text/csv", "AllVitalSigns.csv");

            //using var memoryStream = new MemoryStream();
            //using (var streamWriter = new StreamWriter(memoryStream))
            //{
            //    using var csvWriter = new CsvWriter(streamWriter);
            //    csvWriter.WriteRecords(eventExportDtos);
            //}

            //return memoryStream.ToArray();
        }
    }
}
