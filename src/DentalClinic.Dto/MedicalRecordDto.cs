namespace DentalClinic.Dto;

public class MedicalRecordDto
{
    public int RecordId { get; set; }
    public string? MedicalHistory { get; set; }
    public string? Allergies { get; set; }
    public string? GeneralNotes { get; set; }
    public string Status { get; set; } = string.Empty;

}
