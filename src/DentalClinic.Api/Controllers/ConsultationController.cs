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
[Route("api/consultations")]
public class ConsultationController : ControllerBase
{
    private readonly IConsultationFacade _consultationFacade;

    public ConsultationController(IConsultationFacade consultationFacade)
    {
        _consultationFacade = consultationFacade;
    }


    [HttpGet("record/{recordId}")]
    [Authorize(Policy = AuthorizationPolicies.CanManageConsultations)]
    public async Task<IActionResult> GetByRecordId(int recordId)
    {
        var consultations = await _consultationFacade.GetByRecordIdAsync(recordId);
        var response = ConsultationMapper.ToResponse(consultations);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.CanManageConsultations)]
    public async Task<IActionResult> Create([FromBody] CreateConsultationRequest request)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var dto = ConsultationMapper.ToDto(request);
            var result = await _consultationFacade.CreateAsync(dto, userId);
            var response = ConsultationMapper.ToResponse(result);
            return CreatedAtAction(nameof(GetByRecordId), new { recordId = request.record_id }, response);
        }
        catch (BadRequestResponseException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.CanManageConsultations)]
    public async Task<IActionResult> GetAll()
    {
        var consultations = await _consultationFacade.GetAllConsultations();
        var response = ConsultationMapper.ToSummaryResponse(consultations);
        return Ok(response);
    }
}
