using System;
using DentalClinic.Domain.Entities;

namespace DentalClinic.Infrastructure.Repositories;

public interface IRoleRepository
{
    Task<List<Role>> GetAllAsync();
}
