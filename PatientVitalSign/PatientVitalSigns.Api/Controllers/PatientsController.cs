using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientVitalSigns.Application;
using PatientVitalSigns.Domain;
using PatientVitalSigns.Infrastucture;
using System;

namespace PatientVitalSigns.Api;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
    {
    private readonly IMediator _mediator;

    public PatientsController(IMediator mediator)
        {
        _mediator = mediator;
        }

    // GET /patients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients()
        {
        var result = await _mediator.Send(new GetPatientsQuery());
        return Ok(result);
        }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientDto>> GetPatient(int id)
        {
        var result = await _mediator.Send(new GetPatientQuery(id));
        return Ok(result);
        }

    // GET /patients/{id}/vitals
    [HttpGet("{id}/vitals")]
  public async Task<ActionResult<VitalSignsDto>> GetPatientVitals(int id)
    {
    var result = await _mediator.Send(new GetPatientVitalsQuery(id));
    if (result == null)
      return NotFound();
    return Ok(result);
    }

    [HttpGet("GetAllVitalsCsv")]
    public async Task<ActionResult<byte[]>> GetAllVitalsCsv()
        {
        var exportdata = await _mediator.Send(new GetAllPatientVitalsQuery());
        if (exportdata == null)
            return NotFound();
        
        var file = new CsvExporter().ExportEventsToCsv(exportdata.ToList());

        return Ok(file);
        }

    // POST /patients/{id}/vitals
    [HttpPost("{id}/vitals")]
  public async Task<ActionResult<PostPatientVitalsResultDto>> PostPatientVitals(int id, [FromBody] PostPatientVitalsDto vitals)
    {
    var command = new CreatePatientVitalsCommand(id, vitals);
    var result = await _mediator.Send(command);
    if (!result.Success)
      return BadRequest(result);
    return Ok(result);
    }
  }