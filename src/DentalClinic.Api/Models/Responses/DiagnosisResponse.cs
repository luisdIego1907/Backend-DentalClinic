namespace DentalClinic.Api.Models.Responses;

public class DiagnosisResponse
{
    public int diagnosis_id { get; set; }
    public string description { get; set; } = string.Empty;
    public DateOnly diagnosis_date { get; set; }
}
