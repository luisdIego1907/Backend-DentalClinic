namespace DentalClinic.Dto;

public class UserDto
{

    public Guid user_resource_id {get;set;}
    public required string first_name {get;set;}

    public required string last_name {get;set;}

    public required string username {get;set;}

    public required string email {get;set;}

    
}
