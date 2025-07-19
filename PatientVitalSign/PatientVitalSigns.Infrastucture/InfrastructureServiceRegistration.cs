
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientVitalSigns.Domain;
using PatientVitalSignsSolution.Infrastucture;

namespace PatientVitalSigns.Infrastucture
{
    public static class InfrastructureServiceRegistration
        {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICsvExporter, CsvExporter>();
            return services;    
        }
    }
}
