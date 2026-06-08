using DentalClinic.Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Role>> GetAllAsync()
    {
        return await _context.Roles.ToListAsync();
    }

}
