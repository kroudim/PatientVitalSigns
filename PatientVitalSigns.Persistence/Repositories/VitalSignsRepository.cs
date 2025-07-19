
using Microsoft.EntityFrameworkCore;
using PatientVitalSigns.Domain;

namespace PatientVitalSigns.Persistence
  {
  public class VitalSignsRepository : IVitalSignsRepository
    {
    private readonly PatientVitalSignsContext _context;

    public VitalSignsRepository(PatientVitalSignsContext context)
      {
      _context = context;
      }

    public Task<IEnumerable<VitalSigns>> GetByPatientIdAsync(int patientId)
      {
      var vitals = _context.VitalSigns.Where(v => v.PatientId == patientId);
      return Task.FromResult(vitals.AsEnumerable());
      }

    public Task AddAsync(VitalSigns vitalSigns)
      {
      // Assign a unique Id if needed
      if (vitalSigns.Id == 0)
        {
        vitalSigns.Id = _context.VitalSigns.Count() == 0 ? 1 : _context.VitalSigns.Max(v => v.Id) + 1;
        }
      _context.VitalSigns.Add(vitalSigns);
      _context.SaveChanges();

      return Task.CompletedTask;
      }

    // Other methods as needed
    }
  }
