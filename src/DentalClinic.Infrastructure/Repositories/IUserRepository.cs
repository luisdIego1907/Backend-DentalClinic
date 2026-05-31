using DentalClinic.Domain.Entities;

namespace DentalClinic.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();

    Task<User?> GetByResourceIdAsync(Guid user_id);

    Task<User> AddAsync(User user);

    Task<User> CreateAsync(User user);

    Task DeleteAsync(User user);

    Task<bool> HasUserByUsernameAsync(string username);

    Task<bool> HasUserByEmailAsync(string email);

    Task<User?> GetByUserName(string username);
}
