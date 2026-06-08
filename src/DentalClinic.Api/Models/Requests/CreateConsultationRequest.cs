using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.Models.Requests;

public class CreateConsultationRequest
{
    [Required]
    public int record_id { get; set; }

    public int? appointment_id { get; set; }

    [Required]
    public DateOnly consultation_date { get; set; }

    [Required]
    [MaxLength(120)]
    public string reason { get; set; } = string.Empty;

    public string? observations { get; set; }

    public string? odontogram { get; set; }

    [Required]
    public List<DiagnosisRequest> diagnoses { get; set; } = new();

    [Required]
    public List<TreatmentRequest> treatments { get; set; } = new();
}
