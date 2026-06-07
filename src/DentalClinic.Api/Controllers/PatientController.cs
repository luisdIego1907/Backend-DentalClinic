using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DentalClinic.Api.Mappers;
using DentalClinic.Api.Models.Requests;
using DentalClinic.Exceptions;
using DentalClinic.Facade;
using Microsoft.AspNetCore.Authorization;
using DentalClinic.Api.Models.Requests;


namespace DentalClinic.Api.Controllers;

[ApiController]
[Route("api/patients")]
[Authorize]
public class PatientController : ControllerBase
{
    private readonly IPatientFacade patientFacade;

    public PatientController(IPatientFacade patientFacade)
    {
        this.patientFacade = patientFacade;
    }

    [HttpGet]
    [Authorize(Roles = "admin,odontologist,assistant")]
    public async Task<IActionResult> GetPatients()
    {
        var patients = await patientFacade.GetAllAsync();

        var models = PatientMapper.ToModel(patients);

        return Ok(models);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin,odontologist,assistant")]
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
    [Authorize(Roles = "admin,odontologist,assistant")]
    public async Task<IActionResult> AddPatient([FromBody] CreatePatientRequestModel patient)
    {
        var dto = PatientMapper.ToDto(patient);

        var addedPatient = await patientFacade.AddAsync(dto);

        var model = PatientMapper.ToModel(addedPatient);

        return CreatedAtAction(nameof(GetPatient), new { id = model.patient_id }, model);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin,assistant")]
    public async Task<IActionResult> UpdatePatient(
     int id,
     [FromBody] UpdatePatientRequestModel patient)
    {
        try
        {
            var dto = PatientMapper.ToDto(patient);

            await patientFacade.UpdateAsync(id, dto);

            return NoContent();
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
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
