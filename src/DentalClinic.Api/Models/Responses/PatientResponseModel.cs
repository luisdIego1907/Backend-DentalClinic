namespace DentalClinic.Api.Models.Responses;

public class PatientResponseModel
{
  public int patient_id { get; set; }

  public string identification { get; set; } = string.Empty;
  public string first_name { get; set; } = string.Empty;

  public string last_name { get; set; } = string.Empty;

  public DateOnly birth_date { get; set; }

  public string phone { get; set; } = string.Empty;

  public string email { get; set; } = string.Empty;

  public string address { get; set; } = string.Empty;

  public string gender { get; set; } = string.Empty;

  public DateTime created_at { get; set; }

  public string status { get; set; } = "active";
}
