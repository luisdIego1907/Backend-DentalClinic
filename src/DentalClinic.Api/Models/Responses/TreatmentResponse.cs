namespace DentalClinic.Api.Models.Responses;

public class TreatmentResponse
{
    public int treatment_id { get; set; }
    public string description { get; set; } = string.Empty;
    public decimal cost { get; set; }
    public string status { get; set; } = string.Empty;
    public DateOnly start_date { get; set; }
    public DateOnly? end_date { get; set; }

}
