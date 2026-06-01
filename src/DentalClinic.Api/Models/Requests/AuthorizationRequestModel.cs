using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.Models.Requests;

public class AuthorizationRequestModel
{
    [Required]
    [MaxLength(100)]
    public required string username { get; set; }

    [Required]
    [MaxLength(255)]
    public required string password { get; set; }
}
