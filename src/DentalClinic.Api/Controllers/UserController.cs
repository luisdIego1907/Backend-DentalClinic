using DentalClinic.Api.Mappers;
using DentalClinic.Api.Models.Requests;
using DentalClinic.Api.Security;
using DentalClinic.DomainService;
using DentalClinic.Facade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController(IUserFacade userFacade) : ControllerBase
{
    //[Authorize(Policy = AuthorizationPolicies.CanSearchUsers)]
        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var users = await userFacade.GetAllAsync();

            var models = UserMapper.ToModel(users);

            return Ok(models);
        }

        
        [Authorize(Roles = RoleNames.ADMINISTRATOR)]
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequestModel user)
        {
            var requestDto = UserMapper.ToDto(user);

            var userDto = await userFacade.CreateAsync(requestDto);

            var userModel = UserMapper.ToModel(userDto);

            return Ok(userModel);
        }

        
        [Authorize(Roles = RoleNames.ADMINISTRATOR)]
        [HttpGet("{userId}/roles")]
        public async Task<IActionResult> GetUserRolesAsync(Guid userId)
        {
            var userRoles = await userFacade.GetUserRolesAsync(userId);

            var responseModel = UserMapper.ToUserRolesResponseModel(userRoles);
            return Ok(responseModel);
        }

        
        [Authorize(Roles = RoleNames.ADMINISTRATOR)]
        [HttpPut("{userId}/roles")]
        public async Task<IActionResult> UpdateUserRolesAsync(Guid userId, [FromBody] UpdateRolesRequestModel model)
        {
            var requestDto = UserMapper.ToDto(model);
            var userRoles = await userFacade.UpdateUserRolesAsync(userId, requestDto);
            var responseModel = UserMapper.ToUserRolesResponseModel(userRoles);
            return Ok(responseModel);
        }

        [Authorize(Roles = RoleNames.ADMINISTRATOR)]
        [HttpDelete("{userId}/roles")]
        public async Task<IActionResult> DeleteUserRolesAsync(Guid userId)
        {

            await userFacade.DeleteUserRolesAsync(userId);
            return Ok();
        }

}
