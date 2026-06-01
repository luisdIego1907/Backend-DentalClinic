using DentalClinic.DomainService;
using DentalClinic.Api.Enumerations;
using DentalClinic.Exceptions;
namespace DentalClinic.Api.Mappers;

public class RoleMapper
{
    public static string MapRoleAliasToName(RoleAliases alias)
    {
        return alias switch
        {
          RoleAliases.ADMIN => RoleNames.ADMINISTRATOR,
          RoleAliases.ODO => RoleNames.ODONTOLOGIST,
          RoleAliases.ASSIS => RoleNames.ASSISTANT,
          _ => throw new BadRequestResponseException("Invalid role privided")  
        };
    }

    public static RoleAliases MapRoleNameToAlias(string name)
    {
        return name switch
        {
            RoleNames.ADMINISTRATOR => RoleAliases.ADMIN,
            RoleNames.ODONTOLOGIST => RoleAliases.ODO,
            RoleNames.ASSISTANT => RoleAliases.ASSIS,
            _ => throw new BadRequestResponseException("Invalid role provided")
        };
    }
}
