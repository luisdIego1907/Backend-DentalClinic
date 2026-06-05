using DentalClinic.Api.Mappers;
using DentalClinic.Api.Security;
using DentalClinic.Exceptions;
using DentalClinic.Facade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Api.Controllers;

[ApiController]
[Route("api/medical-records")]
[Authorize]
public class MedicalRecordController : ControllerBase
{
    private readonly IMedicalRecordFacade _medicalRecordFacade;

    public MedicalRecordController(IMedicalRecordFacade medicalRecordFacade)
    {
        _medicalRecordFacade = medicalRecordFacade;
    }

    [HttpGet("patient/{patientId}")]
    [Authorize(Policy = AuthorizationPolicies.CanManageMedicalRecords)]
    public async Task<IActionResult> GetByPatientId(int patientId)
    {
        try
        {
            var dto = await _medicalRecordFacade.GetByPatientIdAsync(patientId);
            var response = MedicalRecordMapper.ToResponse(dto);
            return Ok(response);
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}