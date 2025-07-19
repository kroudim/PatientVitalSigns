
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientVitalSigns.Domain
  {
  public interface IVitalSignsRepository
    {
    Task<IEnumerable<VitalSigns>> GetByPatientIdAsync(int patientId);
    Task AddAsync(VitalSigns vitalSigns);
    // Other methods as needed
    }
  }
