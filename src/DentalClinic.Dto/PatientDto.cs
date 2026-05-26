namespace DentalClinic.Dto;

public class PatientDto
{
    public int patient_id{get; set;}
    
    public string first_name{get;set;} = string.Empty;

    public string last_name{get;set;} = string.Empty;

    public string identification{get;set;} = string.Empty;

    public string phone{get;set;} = string.Empty;

    public string address{get;set;} = string.Empty;

    public DateOnly birth_date { get; set; }

     public string email { get; set; } = string.Empty;

      public string gender { get; set; } = string.Empty;

        public DateTime created_at { get; set; }



}
