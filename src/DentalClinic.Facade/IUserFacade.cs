using DentalClinic.Dto;

namespace DentalClinic.Facade;

public interface IUserFacade
{
    Task<List<UserDto>> GetAllAsync();

    Task<UserDto> CreateAsync(CreateUserDto user);

    Task<UserRolesDto> UpdateUserRolesAsync(Guid userId, UpdateRolesDto dto);

    Task DeleteUserRolesAsync(Guid userId);

    Task<UserRolesDto> GetUserRolesAsync(Guid userId);
}
