using MediatR;

namespace PatientVitalSigns.Application
    {
    // Command to create a new patient, returning the new Patient's Id
    public class CreatePatientCommand : IRequest<int>
        {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Room { get; set; }

        }
    }