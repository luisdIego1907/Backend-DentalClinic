namespace DentalClinic.Dto;

public class TreatmentDto
{
    public int Treatment_id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateOnly Start_date { get; set; }
    public DateOnly? End_date { get; set; }
}
