using DentalClinic.Domain.Entities;

namespace DentalClinic.DomainService;

public interface IRoleService
{
    Task<List<Role>> GetAllAsync();
}
