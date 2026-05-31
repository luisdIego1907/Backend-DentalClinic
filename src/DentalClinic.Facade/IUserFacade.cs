using DentalClinic.Dto;

namespace DentalClinic.Facade;

public interface IUserFacade
{
    Task<List<UserDto>> GetAllAysnc();

    Task<UserDto> CreateAsync(CreateUserDto user);

    Task<UserRolesDto> UpdateUserRolesAsync(Guid userId, UpdateRolesDto dto);

    Task DeleteUserRolesAsync(Guid userId);
}
