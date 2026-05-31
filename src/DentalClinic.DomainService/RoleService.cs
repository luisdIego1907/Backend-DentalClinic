using DentalClinic.Domain.Entities;
using DentalClinic.Infrastructure.Repositories;

namespace DentalClinic.DomainService;

public class RoleService : IRoleService
{

    private readonly IRoleRespository roleRespository;

    public RoleService(IRoleRespository roleRespository)
    {
        this.roleRespository = roleRespository;
    }
    public Task<List<Role>> GetAllAsync()
    {
        return roleRespository.GetAllAsync();
    }

}
