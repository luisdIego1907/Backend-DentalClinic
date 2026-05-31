using System;
using DentalClinic.Domain.Entities;

namespace DentalClinic.Infrastructure.Repositories;

public interface IRoleRespository
{
    Task<List<Role>> GetAllAsync();
}
