<h2 style="color:brown">A real-time Patient Vital Signs Monitoring Dashboard using ASP.NET Core with Razor Views. </h2>
**Technology Stack:** ASP.NET Core, Razor Views, SignalR, Entity Framework Core, SQLite

## Requirements

### Core Functionality
Create a web application that displays patient vital signs in real-time with the following features:

1. **Patient Management**
   - List of patients with basic information (Name, Age, Room Number)
   - Ability to select a patient for monitoring

2. **Real-time Vital Signs Dashboard**
   - Display current vital signs (Heart Rate, Blood Pressure, Oxygen Saturation)
   - Color-coded status indicators (Normal/Warning/Critical)
   - Live updates without page refresh
   - Alert notifications for critical values

3. **Data Visualization**
   - Simple charts showing vital sign trends over time
   - Historical data view for the last 24 hours

### Technical Requirements

#### Backend 
- **Models:** Patient and VitalSigns entities
- **API Endpoints:** 
  - GET /patients
  - GET /patients/{id}/vitals
  - POST /patients/{id}/vitals (for simulation)
- **SignalR Hub:** Real-time vital signs broadcasting
- **EF Core:** SQLite database with proper relationships
- **Validation:** Input validation and business rules
- **Data Seeding:** Sample patients and initial vital signs

#### Frontend 
- **Razor Views:** Clean, responsive UI using Bootstrap
- **JavaScript/SignalR Client:** Real-time connection handling
- **Interactive Elements:** Patient selection, alert management
- **Charts:** Use Chart.js or any other framework for vital signs visualization
- **Responsive Design:**

#### Testing 
- **Unit Tests:** Backend services and validation logic

- **PatientVitalSigns.Api** – Hosts API endpoints for patient and vital signs operations, providing clean separation for RESTful interactions.
- **PatientVitalSigns.Application** – Contains business logic, use cases, and service interfaces, ensuring the core logic is independent of frameworks and UI.
- **PatientVitalSigns.Domain** – Defines the core domain models and business rules, making the solution highly testable and extensible.
- **PatientVitalSigns.Infrastructure** – Implements external integrations such as SignalR hubs and other service implementations.
- **PatientVitalSigns.Persistence** – Handles data access with Entity Framework Core and maintains database context, migrations, and data seeding.
- **PatientVitalSigns.Test** – Provides unit tests for business logic and validation, ensuring high code coverage and robustness.
- **PatientVitalSignsSolution.WebApp** – Delivers the frontend Razor Views, responsive UI, real-time dashboards, and data visualization components.

This layered and modular architecture allows for clear separation of concerns, easy testing, and future scalability.

The solution is ready to run with `dotnet run`, includes database and sample data, and comes with comprehensive documentation explaining setup, architecture, and API usage.
The solution  has two startup projects, the  PatientVitalSigns.Api that will start on https://localhost:7198 and  PatientVitalSignsSolution.WebApp that will start on https://localhost:7049
To build the solution and run the those two projects execute the script buildrun.ps1 that is located on the root folder of the solution
To just run those two projects execute the script  run.ps1  that is located on the root folder of the solution  
