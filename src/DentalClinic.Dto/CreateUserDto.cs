namespace DentalClinic.Dto;

public class CreateUserDto
{
    public required string first_name {get;set;}

    public required string last_name {get;set;}

    public required string username {get;set;}

    public required string email {get;set;}

    public required string password {get;set;}
}
