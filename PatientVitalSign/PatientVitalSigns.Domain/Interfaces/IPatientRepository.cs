
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientVitalSigns.Domain
  {
  public interface IPatientRepository
    {
    Task<Patient> GetByIdAsync(int id);
    Task<IEnumerable<Patient>> GetAllAsync();
    Task AddAsync(Patient patient);
    // Other methods as needed
    }
  }
