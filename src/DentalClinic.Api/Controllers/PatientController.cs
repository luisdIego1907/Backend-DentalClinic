using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DentalClinic.Api.Mappers;
using DentalClinic.Api.Models.Requests;
using DentalClinic.Exceptions;
using DentalClinic.Facade;

namespace DentalClinic.Api.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientController : ControllerBase
{
    private readonly IPatientFacade patientFacade;

    public PatientController(IPatientFacade patientFacade)
    {
        this.patientFacade = patientFacade;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients()
    {
        var patients = await patientFacade.GetAllAsync();

        var models = PatientMapper.ToModel(patients);

        return Ok(models);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        try
        {
            var patient = await patientFacade.GetByIdAsync(id);
            
            var model = PatientMapper.ToModel(patient);

            return Ok(model);
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPatient([FromBody] CreatePatientRequestModel patient)
    {
        var dto = PatientMapper.ToDto(patient);
        
        var addedPatient = await patientFacade.AddAsync(dto);

        var model = PatientMapper.ToModel(addedPatient);
        
        return CreatedAtAction(nameof(GetPatient), new {id = model.patient_id}, model);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        try
        {
            await patientFacade.DeleteAsync(id);

            return Ok();
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }
}
