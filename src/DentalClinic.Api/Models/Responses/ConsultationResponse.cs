namespace DentalClinic.Api.Models.Responses;

public class ConsultationResponse
{
    public int consultation_id { get; set; }
    public DateOnly consultation_date { get; set; }
    public string reason { get; set; } = string.Empty;
    public string? observations { get; set; }
    public string? odontogram { get; set; }
    public List<DiagnosisResponse> diagnoses { get; set; } = new();
    public List<TreatmentResponse> treatments { get; set; } = new();
}
