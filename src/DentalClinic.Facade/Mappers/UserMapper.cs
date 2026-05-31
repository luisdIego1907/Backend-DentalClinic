using DentalClinic.Domain.Entities;
using DentalClinic.Dto;

namespace DentalClinic.Facade.Mappers;

public class UserMapper
{
    public static List<UserDto> ToDto(List<User> users)
    {
        return users.Select(u => ToDto(u)).ToList();
    }

    public static UserDto ToDto(User user)
    {
        return new UserDto
        {
            user_resource_id = user.user_resource_id,
            first_name = user.first_name,
            last_name = user.last_name,
            username = user.username,
            email = user.email
        };
    }

    public static UserRolesDto ToUserRolesDto(User user)
    {
        return new UserRolesDto
        {
            Roles = user.UserRoles?.Select(ur=>ur.role.Name).ToList() ?? [],
        };
    }
}
