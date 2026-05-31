using DentalClinic.Domain.Entities;
using DentalClinic.Dto;

namespace DentalClinic.DomainService;

public interface IUserService
{
    Task<List<User>> GetAllAsync();

    Task<User> AddAsync(UserDto user);

   Task<User?> GetByResourceIdAsync(Guid id);

   Task<User> CreateAsync(CreateUserDto user);

   Task<User?> GetByUserAndPassword(AuthorizationRequestDto request);
}
