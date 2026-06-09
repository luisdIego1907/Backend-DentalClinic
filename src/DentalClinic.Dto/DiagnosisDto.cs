namespace DentalClinic.Dto;

public class DiagnosisDto
{
    public int Diagnosis_id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateOnly Diagnosis_date { get; set; }
}
