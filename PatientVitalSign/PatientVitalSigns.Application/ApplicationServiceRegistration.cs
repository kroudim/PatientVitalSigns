using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace PatientVitalSigns.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
         services.AddMediatR(
           cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

      return services;
        }
    }
}
