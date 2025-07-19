using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PatientVitalSigns.Domain;
using FluentValidation;

namespace PatientVitalSigns.Application
    {
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, int>
        {
        private readonly IPatientRepository _patientRepository;

        public CreatePatientCommandHandler(IPatientRepository patientRepository)
            {
            _patientRepository = patientRepository;
            }

        public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
            {
            var patient = new Patient
                {
                Name = request.Name,
                SurName = request.SurName,
                Room = request.Room
                };

            await _patientRepository.AddAsync(patient);

            return patient.Id;
            }
        }
    }