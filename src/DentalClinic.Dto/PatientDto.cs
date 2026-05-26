namespace DentalClinic.Dto;

public class PatientDto
{
    public int patient_id{get; set;}
    
    public string first_name{get;set;} = string.Empty;

    public string last_name{get;set;} = string.Empty;

    public string identification{get;set;} = string.Empty;

    public string phone{get;set;} = string.Empty;

    public string address{get;set;} = string.Empty;
}
