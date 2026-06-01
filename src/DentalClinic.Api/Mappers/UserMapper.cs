using DentalClinic.Api.Models.Requests;
using DentalClinic.Api.Models.Responses;
using DentalClinic.Domain.Entities;
using DentalClinic.Dto;

namespace DentalClinic.Api.Mappers;

public class UserMapper
{
    public static List<UserResponseModel> ToModel(List<UserDto> users)
    {
        return users.Select(u => ToModel(u)).ToList();
    }

    public static UserResponseModel ToModel(UserDto user)
    {
        return new UserResponseModel
        {
          user_resoruce_id = user.user_resource_id,
          first_name = user.first_name,
          last_name = user.last_name,
          email = user.email,
          username = user.username  
        };
    }

    public static CreateUserDto ToDto(CreateUserRequestModel user)
    {
        return new CreateUserDto
        {
            first_name = user.first_name,
            last_name = user.last_name,
            email = user.email,
            username = user.email,
            password = user.password
        };
    }

    public static UserRolesResponseModel ToDto(UserRolesDto dto)
    {
        return new UserRolesResponseModel
        {
          Roles = dto.Roles?.Select(r => RoleMapper.MapRoleNameToAlias(r)).ToList() ?? [],  
        };
    }

    public static UpdateRolesDto ToDto(UpdateRolesRequestModel model)
    {
        return new UpdateRolesDto
        {
            Roles = model.Roles?.Distinct().Select(r => RoleMapper.MapRoleAliasToName(r)).ToList() ?? [],
        };
    }

    public static UserRolesResponseModel ToUserRolesResponseModel(UserRolesDto model)
    {
        return new UserRolesResponseModel
        {
            Roles = model.Roles?.Select(r => RoleMapper.MapRoleNameToAlias(r)).ToList() ?? [],
        };
    }
}
