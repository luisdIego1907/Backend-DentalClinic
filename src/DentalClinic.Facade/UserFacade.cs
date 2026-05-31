using DentalClinic.Domain.Entities;
using DentalClinic.DomainService;
using DentalClinic.Dto;
using DentalClinic.Exceptions;
using DentalClinic.Facade.Mappers;
using DentalClinic.Infrastructure;

namespace DentalClinic.Facade;

public class UserFacade : IUserFacade
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    private readonly AppDbContext _context;

    public UserFacade( IUserService userServise, IRoleService roleService, AppDbContext dbContext)
    {
        _userService = userServise;
        _roleService = roleService;
        _context = dbContext;
    }
    public async Task<UserDto> CreateAsync(CreateUserDto user)
    {
       var entity = await _userService.CreateAsync(user);

       await _context.SaveChangesAsync();

        return UserMapper.ToDto(entity);
    }

    public async Task DeleteUserRolesAsync(Guid userId)
    {
        var user = await _userService.GetByResourceIdAsync(userId);

        if(user == null)
        {
            throw new ResourceNotFoundException();
        }

        user.ClearRoles();

        await _context.SaveChangesAsync();
    }

    public async Task<List<UserDto>> GetAllAysnc()
    {
        var entities = await _userService.GetAllAsync();

        return UserMapper.ToDto(entities);
    }

    public async Task<UserRolesDto> UpdateUserRolesAsync(Guid userId, UpdateRolesDto dto)
    {
        List<Role>? allRoles = null;

        if (dto.Roles?.Count > 0)
        {
            allRoles = await _roleService.GetAllAsync();

            if (dto.Roles.Any(role => !allRoles.Any(e => e.Name.Equals(role))))
            {
                throw new BadRequestResponseException("One or more roles do not exists");
            }
        }

        var user = await _userService.GetByResourceIdAsync(userId);

        if(user == null)
        {
            throw new ResourceNotFoundException();
        }

        user.ClearRoles();

        if (dto.Roles?.Count > 0)
        {
            allRoles ??= await _roleService.GetAllAsync();

            var matchedRoles = allRoles.Where(r => dto.Roles.Any(role => r.Name.Equals(role))).ToList();

            var userRoles =matchedRoles.Select(role => new UserRole
            {
                user = user,
                role = role,
            }).ToList();

            user.UserRoles.AddRange(userRoles);
        }

        await _context.SaveChangesAsync();

        return UserMapper.ToUserRolesDto(user);
    }

}
