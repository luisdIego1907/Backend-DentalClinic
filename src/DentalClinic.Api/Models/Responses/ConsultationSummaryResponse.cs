namespace DentalClinic.Api.Models.Responses;

public class ConsultationSummaryResponse
{
    public int consultation_id { get; set; }
    public DateOnly consultation_date { get; set; }
    public string reason { get; set; } = string.Empty;
    public string? observations { get; set; }
    public string? odontogram { get; set; }
    public string odontologist_first_name { get; set; } = string.Empty;
    public string odontologist_last_name { get; set; } = string.Empty;
    public string patient_first_name { get; set; } = string.Empty;
    public string patient_last_name { get; set; } = string.Empty;
    public List<DiagnosisResponse> diagnoses { get; set; } = new();
    public List<TreatmentResponse> treatments { get; set; } = new();
}
