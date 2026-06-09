namespace DentalClinic.Api.Models.Responses;

public class AppointmentResponseModel
{
    public int id { get; set; }

    public PatientResponseModel? patient { get; set; }

    public string doctor { get; set; } = string.Empty;

    public string doctorUserResourceId { get; set; } = string.Empty;

    public string date { get; set; } = string.Empty;

    public string time { get; set; } = string.Empty;

    public int durationMinutes { get; set; }

    public string reason { get; set; } = string.Empty;

    public string status { get; set; } = string.Empty;
}
