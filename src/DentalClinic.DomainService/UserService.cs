using DentalClinic.Domain.Entities;
using DentalClinic.Dto;
using DentalClinic.Infrastructure.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DentalClinic.DomainService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public Task<User> AddAsync(UserDto user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateAsync(CreateUserDto user)
    {
        if (await _userRepository.HasUserByUsernameAsync(user.username))
        {
            throw new Exceptions.BadRequestResponseException("username is already taken");
        }

        if (await _userRepository.HasUserByEmailAsync(user.email))
        {
            throw new Exceptions.BadRequestResponseException("Email is already taken");
        }

        var entity = new User
        {
          user_resource_id = Guid.NewGuid(),  

          first_name = user.first_name,

          last_name = user.last_name,

          username = user.username,

          email = user.email,

          password_hash =  BCrypt.Net.BCrypt.HashPassword(user.password),
        };

        return await _userRepository.CreateAsync(entity);
    }

    public Task<List<User>> GetAllAsync()
    {
        return _userRepository.GetAllAsync();
    }

    public Task<User?> GetByResourceIdAsync(Guid id)
    {
        return _userRepository.GetByResourceIdAsync(id);
    }

    public async Task<User?> GetByUserAndPassword(AuthorizationRequestDto request)
    {
        var user = await _userRepository.GetByUserName(request.username);

        if(user == null)
        {
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(request.password, user.password_hash))
        {
            return null;
        }

        return user;
    }

}
