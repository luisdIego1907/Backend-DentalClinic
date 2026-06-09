namespace DentalClinic.Api.Models.Requests;

public class SaveAppointmentRequestModel
{
    public int patient_id { get; set; }

    public Guid doctor_user_resource_id { get; set; }

    public string appointment_date { get; set; } = string.Empty;

    public string appointment_time { get; set; } = string.Empty;

    public int duration_minutes { get; set; }

    public string reason { get; set; } = string.Empty;

    public string status { get; set; } = "Pendiente";

    public string? notes { get; set; }
}
