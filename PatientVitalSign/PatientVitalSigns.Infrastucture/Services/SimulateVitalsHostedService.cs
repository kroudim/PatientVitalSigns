using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net.Http.Json;
using PatientVitalSigns.Application;
using Microsoft.AspNetCore.SignalR;
using PatientVitalSignsSolution.Infrastucture;

namespace PatientVitalSignsSolution.Infrastucture
    {
    public class SimulateVitalsHostedService : BackgroundService
        {
        private readonly IServiceProvider _services;
        private readonly ILogger<SimulateVitalsHostedService> _logger;
        private readonly string _apiBaseUrl;
        private List<PatientDto> _patients = new();

        public SimulateVitalsHostedService(IServiceProvider services, ILogger<SimulateVitalsHostedService> logger, IConfiguration configuration)
            {
            _services = services;
            _logger = logger;
            _apiBaseUrl = "https://localhost:7198/api/patients";
            }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
            while (!stoppingToken.IsCancellationRequested)
                {
                using (var scope = _services.CreateScope())
                    {
                    var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<VitalSignsHub>>();
                    var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
                    var httpClient = httpClientFactory.CreateClient();

                    try
                        {
                        if (_patients == null || !_patients.Any())
                            {
                            _patients = await httpClient.GetFromJsonAsync<List<PatientDto>>(_apiBaseUrl, stoppingToken) ?? new List<PatientDto>();
                            if (!_patients.Any())
                                {
                                _logger.LogWarning("No patients found to simulate vitals for.");
                                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
                                continue;
                                }
                            }

                        foreach (var patient in _patients)
                            {
                            var vitals = new PostPatientVitalsDto
                                {
                                HeartRate = Random.Shared.Next(55, 130),
                                BloodPressureDiastolic = Random.Shared.Next(60, 95),
                                BloodPressureSystolic = Random.Shared.Next(90, 150),
                                OxygenSaturation = Random.Shared.Next(88, 100)
                                };

                            var response = await httpClient.PostAsJsonAsync($"{_apiBaseUrl}/{patient.Id}/vitals", vitals, stoppingToken);

                            if (response.IsSuccessStatusCode)
                                {
                                await hubContext.Clients.All.SendAsync("ReceiveVitalSigns", patient.Id, new
                                    {
                                    heartRate = vitals.HeartRate,
                                    bloodPressureDiastolic = vitals.BloodPressureDiastolic,
                                    bloodPressureSystolic = vitals.BloodPressureSystolic,
                                    oxygenSaturation = vitals.OxygenSaturation,
                                    timestamp = DateTime.UtcNow,
                                    heartRateStatus = vitals.HeartRateStatus.ToString(),
                                    bloodPressureStatus = vitals.BloodPressureStatus.ToString(),
                                    oxygenSaturationStatus = vitals.OxygenSaturationStatus.ToString()
                                    });
                                }
                            else
                                {
                                var errorContent = await response.Content.ReadAsStringAsync();
                                _logger.LogWarning("Failed to post vitals for patient {PatientId}: {Error}", patient.Id, errorContent);
                                }
                            }
                        }
                    catch (Exception ex)
                        {
                        _logger.LogError(ex, "Error during vitals simulation.");
                        }
                    }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }
    }