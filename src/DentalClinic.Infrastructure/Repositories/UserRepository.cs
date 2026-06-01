using DentalClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);

        return user;
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByResourceIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u =>  u.user_resource_id == id);
    }

    public Task<User?> GetByUserName(string username)
    {
        return _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.role).FirstOrDefaultAsync(u => u.username == username);
    }

    public async Task<bool> HasUserByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.email == email);
    }

    public async Task<bool> HasUserByUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.username == username);
    }
     public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        return user;
    }
}
