using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.Models.Requests;

public class DiagnosisRequest
{
    [Required]
    [MaxLength(200)]
    public string description { get; set; } = string.Empty;

    [Required]
    public DateOnly diagnosis_date { get; set; }
}
