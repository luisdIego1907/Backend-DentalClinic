using DentalClinic.Domain.Entities;
using DentalClinic.Infrastructure.Repositories;

namespace DentalClinic.DomainService;

public class RoleService : IRoleService
{

    private readonly IRoleRepository roleRespository;

    public RoleService(IRoleRepository roleRespository)
    {
        this.roleRespository = roleRespository;
    }
    public Task<List<Role>> GetAllAsync()
    {
        return roleRespository.GetAllAsync();
    }

}
