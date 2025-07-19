using Microsoft.AspNetCore.Mvc;
using PatientVitalSigns.Application;
using PatientVitalSignsSolution.WebApp.Models;
using Microsoft.AspNetCore.SignalR;
using PatientVitalSignsSolution.Infrastucture;
using System.Text;
using PatientVitalSigns.Domain;

namespace PatientVitalSignsSolution.WebApp.Controllers
    {
    public class PatientsController : Controller
        {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly IHubContext<VitalSignsHub> _hubContext;

        public PatientsController(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHubContext<VitalSignsHub> hubContext)
            {
            _httpClient = httpClientFactory.CreateClient();
            _apiBaseUrl = configuration.GetValue<string>("ApiBaseUrl") ?? "https://localhost:7198/api/patients";
            _hubContext = hubContext;
            }

        // GET: /Patients
        public async Task<IActionResult> Index()
            {
            var patients = await _httpClient.GetFromJsonAsync<List<PatientDto>>($"{_apiBaseUrl}");
            return View(patients);
            }

        // GET: /Patients/Monitor/{id}
        public async Task<IActionResult> Monitor(int id)
            {
            var patient = await _httpClient.GetFromJsonAsync<PatientDto>($"{_apiBaseUrl}/{id}");

            var vitals = await _httpClient.GetFromJsonAsync<List<VitalSignsDto>>($"{_apiBaseUrl}/{id}/vitals");
            var viewModel = new PatientMonitorViewModel
                {
                Patient = patient,
                VitalSigns = vitals ?? new List<VitalSignsDto>()
                };
            return View(viewModel);
            }

        public async Task<IActionResult> History(int id)
            {
            var patient = await _httpClient.GetFromJsonAsync<PatientDto>($"{_apiBaseUrl}/{id}");

            var vitals = await _httpClient.GetFromJsonAsync<List<VitalSignsDto>>($"{_apiBaseUrl}/{id}/vitals");
            var viewModel = new PatientMonitorViewModel
                {
                Patient = patient,
                VitalSigns = vitals ?? new List<VitalSignsDto>()
                };
            return View(viewModel);
            }

        [HttpGet]
        public async Task<IActionResult> ExportAllVitalsCsv()
        {
            var bytes =  await _httpClient.GetFromJsonAsync<byte[]>($"{_apiBaseUrl}/GetAllVitalsCsv");
            return File(bytes, "text/csv", "AllVitalSigns.csv");
        }


        [HttpPost]
        public async Task<IActionResult> SimulateVitals(int id, PostPatientVitalsDto vitals)
            {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/{id}/vitals", vitals);
            if (!response.IsSuccessStatusCode)
                {
                ModelState.AddModelError("", "Failed to post vitals");
                // Optionally, handle error details
                }
            else
                {
                // This is the broadcast part:
                await _hubContext.Clients.All.SendAsync("ReceiveVitalSigns", id, new
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

            return RedirectToAction("Monitor", new { id });
            }
        }
    }
