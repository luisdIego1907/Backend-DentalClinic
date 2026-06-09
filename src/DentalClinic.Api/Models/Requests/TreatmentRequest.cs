using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.Models.Requests;

public class TreatmentRequest
{
    [Required]
    [MaxLength(200)]
    public string description { get; set; } = string.Empty;

    [Required]
    public decimal cost { get; set; }

    [Required]
    [MaxLength(20)]
    public string status { get; set; } = string.Empty;

    [Required]
    public DateOnly start_date { get; set; }

    public DateOnly? end_date { get; set; }

}
