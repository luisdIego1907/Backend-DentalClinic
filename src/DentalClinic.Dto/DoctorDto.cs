namespace DentalClinic.Dto;

public class DoctorDto
{
    public Guid user_resource_id { get; set; }

    public string first_name { get; set; } = string.Empty;

    public string last_name { get; set; } = string.Empty;

    public string username { get; set; } = string.Empty;

    public string email { get; set; } = string.Empty;

    public string display_name { get; set; } = string.Empty;
}
