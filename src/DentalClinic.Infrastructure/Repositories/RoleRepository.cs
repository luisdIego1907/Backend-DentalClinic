using DentalClinic.Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using DentalClinic.Domain.Entities;

namespace DentalClinic.Infrastructure.Repositories;

public class RoleRepository : IRoleRespository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }
    public Task<List<Role>> GetAllAsync()
    {
        return _context.Roles.ToListAsync();
    }

}
