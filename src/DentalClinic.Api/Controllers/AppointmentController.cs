using System.Security.Claims;
using DentalClinic.Api.Mappers;
using DentalClinic.Api.Models.Requests;
using DentalClinic.Api.Security;
using DentalClinic.Exceptions;
using DentalClinic.Facade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Api.Controllers;

[ApiController]
[Route("api/appointments")]
[Authorize(Policy = AuthorizationPolicies.CanManageAppointments)]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentFacade appointmentFacade;

    public AppointmentController(IAppointmentFacade appointmentFacade)
    {
        this.appointmentFacade = appointmentFacade;
    }

    [HttpGet]
    public async Task<IActionResult> GetAppointments()
    {
        var appointments = await appointmentFacade.GetAllAsync();

        var models = AppointmentMapper.ToModel(appointments);

        return Ok(models);
    }

    [HttpGet("odontologists")]
    public async Task<IActionResult> GetOdontologists()
    {
        var odontologists = await appointmentFacade.GetOdontologistsAsync();

        var models = AppointmentMapper.ToModel(odontologists);

        return Ok(models);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(int id)
    {
        try
        {
            var appointment = await appointmentFacade.GetByIdAsync(id);

            var model = AppointmentMapper.ToModel(appointment);

            return Ok(model);
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAppointment(
        [FromBody] SaveAppointmentRequestModel appointment)
    {
        var dto = AppointmentMapper.ToDto(appointment);

        var addedAppointment = await appointmentFacade.AddAsync(dto);

        var model = AppointmentMapper.ToModel(addedAppointment);

        return CreatedAtAction(nameof(GetAppointment), new { id = model.id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(
        int id,
        [FromBody] UpdateAppointmentRequestModel appointment)
    {
        try
        {
            var dto = AppointmentMapper.ToDto(appointment);

            await appointmentFacade.UpdateAsync(id, dto);

            return Ok();
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("my-patients")]
    public async Task<IActionResult> GetMyPatients()
    {
        var userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );

        var patients = await appointmentFacade
            .GetPatientsByDoctorAsync(userId);

        var models = PatientMapper.ToModel(patients);

        return Ok(models);
    }
}
