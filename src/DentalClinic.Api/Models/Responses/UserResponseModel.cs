namespace DentalClinic.Api.Models.Responses;

public class UserResponseModel
{
    public Guid user_resoruce_id {get;set;}
    public required string first_name {get;set;}

    public required string last_name {get;set;}

    public required string username {get;set;}

    public required string email {get;set;}
}
