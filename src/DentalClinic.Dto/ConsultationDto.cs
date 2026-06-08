namespace DentalClinic.Dto;

public class ConsultationDto
{
    public int consultation_id { get; set; }
    public DateOnly consultation_date { get; set; }
    public string reason { get; set; } = string.Empty;
    public string? observations { get; set; }
    public string? odontogram { get; set; }
    public List<DiagnosisDto> diagnoses { get; set; } = new List<DiagnosisDto>();
    public List<TreatmentDto> treatments { get; set; } = new List<TreatmentDto>();
}

