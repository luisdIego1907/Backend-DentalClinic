using DentalClinic.Api.Enumerations;

namespace DentalClinic.Api.Models.Responses;

public class UserRolesResponseModel
{
    public List<RoleAliases> Roles {get;set;} = [];
}
