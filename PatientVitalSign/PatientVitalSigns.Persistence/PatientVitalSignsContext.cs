
using Microsoft.EntityFrameworkCore;
using PatientVitalSigns.Domain;

namespace PatientVitalSigns.Persistence
  {
    public class PatientVitalSignsContext : DbContext
    {
        public PatientVitalSignsContext(DbContextOptions<PatientVitalSignsContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<VitalSigns> VitalSigns => Set<VitalSigns>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
      modelBuilder.Entity<VitalSigns>(b =>
      {
          b.OwnsOne(v => v.BloodPressure);
          b.OwnsOne(v => v.HeartRate);
          b.OwnsOne(v => v.OxygenSaturation);

          // Ensure TimeStamp property is mapped as datetime (default for DateTime in EF/SQLite)
          b.Property(v => v.TimeStamp)
              .HasColumnType("datetime")
              .IsRequired();
      });



      }

    }
}
