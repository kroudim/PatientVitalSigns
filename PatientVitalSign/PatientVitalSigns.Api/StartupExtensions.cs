using PatientVitalSigns.Application;
using PatientVitalSigns.Persistence;
using Microsoft.EntityFrameworkCore;
using PatientVitalSigns.Infrastucture;


namespace PatientVitalSigns.Api
    {
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder)
        {

            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();

            return builder.Build();

        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GloboTicket Ticket Management API");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();

           // app.UseCustomExceptionHandler();

            app.UseCors("Open");

            app.UseAuthorization();

            app.MapControllers();

            return app;

        }

        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
          var db = scope.ServiceProvider.GetRequiredService<PatientVitalSignsContext>();
          db.Database.EnsureCreated();
          DbInitializer.Initialize(db);
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}