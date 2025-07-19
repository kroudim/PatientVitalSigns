
using Microsoft.EntityFrameworkCore;
using PatientVitalSigns.Domain;
using System;

namespace PatientVitalSigns.Persistence
  {
  public static class DbInitializer
    {
        public static void Initialize(PatientVitalSignsContext context)
            {
            // Ensure database is created
            context.Database.EnsureCreated();

            // Look for any patients.
            if (context.Patients.Any())
                {
                // DB has been seeded
                return;
                }

            var patients = new Patient[]
            {
                new Patient { Name = "John", SurName = "Doe", Room = 101 },
                new Patient { Name = "Maria", SurName = "Papadopoulou", Room = 102 },
                new Patient { Name = "Alex", SurName = "Smith", Room = 103 }
            };
            context.Patients.AddRange(patients);
            context.SaveChanges();


            for (int i = 0; i < 24; i++)
                {
                var vitalTime = DateTime.UtcNow.AddHours(-i);
                var vitals = new VitalSigns[]
                {
        new VitalSigns { PatientId = patients[0].Id, HeartRate = new HeartRate(Random.Shared.Next(55, 130)), BloodPressure = new BloodPressure(Random.Shared.Next(70, 90), Random.Shared.Next(100, 140)), OxygenSaturation = new OxygenSaturation(Random.Shared.Next(88, 100)), TimeStamp = vitalTime },
        new VitalSigns { PatientId = patients[1].Id, HeartRate = new HeartRate(Random.Shared.Next(55, 130)), BloodPressure = new BloodPressure(Random.Shared.Next(70, 90), Random.Shared.Next(100, 140)), OxygenSaturation = new OxygenSaturation(Random.Shared.Next(88, 100)), TimeStamp = vitalTime },
        new VitalSigns { PatientId = patients[2].Id, HeartRate = new HeartRate(Random.Shared.Next(55, 130)), BloodPressure = new BloodPressure(Random.Shared.Next(70, 90), Random.Shared.Next(100, 140)), OxygenSaturation = new OxygenSaturation(Random.Shared.Next(88, 100)), TimeStamp = vitalTime },
                };
                context.VitalSigns.AddRange(vitals);
                }
            context.SaveChanges();
            }
    }
  }
