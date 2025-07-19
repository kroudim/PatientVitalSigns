
using Microsoft.EntityFrameworkCore;
using PatientVitalSigns.Domain;

namespace PatientVitalSigns.Persistence
  {
  public class PatientRepository : IPatientRepository
    {
    private readonly PatientVitalSignsContext _context;

    public PatientRepository(PatientVitalSignsContext context)
      {
      _context = context;
      }

    public Task<Patient> GetByIdAsync(int id)
      {
      var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
      return Task.FromResult(patient);
      }

    public Task<IEnumerable<Patient>> GetAllAsync()
      {
      return Task.FromResult(_context.Patients.AsEnumerable());
      }

    public Task AddAsync(Patient patient)
      {
      // Assign a unique Id if needed
      if (patient.Id == 0)
        {
        patient.Id = _context.Patients.Count() == 0 ? 1 : _context.Patients.Max(p => p.Id) + 1;
        }
      _context.Patients.Add(patient);
      return Task.CompletedTask;
      }

    // Other methods as needed
    }
  }