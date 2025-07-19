using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientVitalSigns.Domain;

namespace PatientVitalSigns.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
         services.AddDbContext<PatientVitalSignsContext>(options =>
          options.UseSqlite("Data Source=patientvitalsigns.db"));

          services.AddScoped<IPatientRepository,PatientRepository >();
          services.AddScoped<IVitalSignsRepository, VitalSignsRepository>();

          return services;    
        }
    }
}
