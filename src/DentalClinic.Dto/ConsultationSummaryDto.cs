namespace DentalClinic.Dto;

public class ConsultationSummaryDto
{
    public int consultation_id { get; set; }
    public DateOnly consultation_date { get; set; }
    public string reason { get; set; } = string.Empty;
    public string? observations { get; set; }
    public string? odontogram { get; set; }
    public string odontologist_first_name { get; set; } = string.Empty;
    public string odontologist_last_name { get; set; } = string.Empty;
    public List<DiagnosisDto> diagnoses { get; set; } = new();
    public List<TreatmentDto> treatments { get; set; } = new();
}
