namespace DentalClinic.Dto;

public class AppointmentDto
{
    public int appointment_id { get; set; }

    public int patient_id { get; set; }

    public Guid doctor_user_resource_id { get; set; }

    public DateOnly appointment_date { get; set; }

    public TimeOnly appointment_time { get; set; }

    public int duration_minutes { get; set; }

    public string reason { get; set; } = string.Empty;

    public string status { get; set; } = "Pendiente";

    public string? notes { get; set; }

    public PatientDto? patient { get; set; }

    public DoctorDto? doctor { get; set; }
}
