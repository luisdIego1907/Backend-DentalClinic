namespace DentalClinic.Api.Models.Responses;

public class MedicalRecordResponse
{
    public int record_id { get; set; }
    public string? medical_history { get; set; }
    public string? allergies { get; set; }
    public string? general_notes { get; set; }
    public string status { get; set; } = string.Empty;
}
