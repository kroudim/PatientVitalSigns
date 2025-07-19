using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using PatientVitalSigns.Application;
using PatientVitalSigns.Api;
using Assert = Xunit.Assert;

namespace PatientVitalSigns.Api.Tests
    {
    public class PatientsApiControllerTests : IClassFixture<WebApplicationFactory<Program>>
        {
        private readonly HttpClient _client;

        public PatientsApiControllerTests(WebApplicationFactory<Program> factory)
            {
            _client = factory.CreateClient();
            }

        [Fact]
        public async Task Get_All_Patients_Returns_Ok_And_List()
            {
            var response = await _client.GetAsync("/api/patients");
            response.EnsureSuccessStatusCode();

            var patients = JsonConvert.DeserializeObject<List<PatientDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(patients);
            }

        [Fact]
        public async Task Get_Patient_By_Id_Returns_Ok_And_Patient()
            {
            // Arrange: Insert a test patient first or mock repository if possible
            var testId = 1; // This should exist in your test DB or be set up via a fixture

            var response = await _client.GetAsync($"/api/patients/{testId}");
            response.EnsureSuccessStatusCode();

            var patient = JsonConvert.DeserializeObject<PatientDto>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(patient);
            Assert.Equal(testId, patient.Id);
            }

        [Fact]
        public async Task Get_Patient_Vitals_By_Id_Returns_Ok_And_Vitals()
            {
            var testId = 1; // This should exist in your test DB or be set up via a fixture

            var response = await _client.GetAsync($"/api/patients/{testId}/vitals");
            response.EnsureSuccessStatusCode();

            var vitals = JsonConvert.DeserializeObject<List<VitalSignsDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(vitals);
            Assert.Equal(testId, vitals.FirstOrDefault().PatientId);
            }

        [Fact]
        public async Task Post_Patient_Vitals_Returns_Ok_And_Result()
            {
            var testId = 1; // This should exist in your test DB or be set up via a fixture

            var postVitals = new PostPatientVitalsDto
                {
                HeartRate = 80,
                BloodPressureSystolic = 120,
                BloodPressureDiastolic = 80,
                OxygenSaturation = 98.5m,
                };

            var response = await _client.PostAsJsonAsync($"/api/patients/{testId}/vitals", postVitals);
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<PostPatientVitalsResultDto>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(testId, result.PatientId);
            }

        [Fact]
        public async Task Get_All_Vitals_Csv_Returns_Ok_And_ByteData()
            {
            var response = await _client.GetAsync("/api/patients/GetAllVitalsCsv");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsByteArrayAsync();
            Assert.NotNull(content);
            Assert.True(content.Length > 0);
            }
        }
    }