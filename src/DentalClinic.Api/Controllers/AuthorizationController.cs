using DentalClinic.Api.Mappers;
using DentalClinic.Api.Models.Requests;
using DentalClinic.Facade;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Api.Controllers;

[ApiController]
[Route("api/authorization")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationFacade authorizationService;

    public AuthorizationController(IAuthorizationFacade authorizationFacade)
    {
        this.authorizationService = authorizationFacade;
    }

    [HttpPost("authorize")]
    public async Task<IActionResult> Authorize([FromBody] AuthorizationRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var requestDto = AuthorizationMapper.ToDto(request);

        var result = await authorizationService.AuthorizeAsync(requestDto).ConfigureAwait(false);

        return Ok(AuthorizationMapper.ToResponse(result));
    }
}
