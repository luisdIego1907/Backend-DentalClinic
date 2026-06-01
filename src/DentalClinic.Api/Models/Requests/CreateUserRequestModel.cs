using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.Models.Requests;

public class CreateUserRequestModel
{

    [Required]
    [MaxLength(50)]
    public required string first_name { get; set; }

    [Required]
    [MaxLength(50)]
    public required string last_name { get; set; }

    [StringLength(100)]
    [Required]
    public required string email { get; set; }


    [StringLength(50)]
    [Required]
    public required string username { get; set; }

    [Required]
    [MaxLength(255)]
    [RegularExpression(
    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
    ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character."
)]
    public required string password { get; set; }
}
