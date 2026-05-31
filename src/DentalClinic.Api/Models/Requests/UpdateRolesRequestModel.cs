using System.ComponentModel.DataAnnotations;
using DentalClinic.Api.Enumerations;

namespace DentalClinic.Api.Models.Requests;

public class UpdateRolesRequestModel
{
    
    [Required]
    public required List<RoleAlieases> Roles {get;set;} = [];
}
