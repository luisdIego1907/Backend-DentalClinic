using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.Models.Requests;

public class UpdatePatientRequestModel
{
    [Required]
    public string identification { get; set; } = string.Empty;

    [Required]
    public string first_name { get; set; } = string.Empty;

    [Required]
    public string last_name { get; set; } = string.Empty;

    [Required]
    public DateOnly birth_date { get; set; }

    [Required]
    [Phone]
    public string phone { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string email { get; set; } = string.Empty;

    [Required]
    public string address { get; set; } = string.Empty;

    [Required]
    public string gender { get; set; } = string.Empty;

    public string status { get; set; } = "active";
}