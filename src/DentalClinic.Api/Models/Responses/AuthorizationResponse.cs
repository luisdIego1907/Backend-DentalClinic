namespace DentalClinic.Api.Models.Responses;

public class AuthorizationResponse
{
    public required string BearerToken { get; set; }
    public DateTime ExpiresIn { get; set; }
}

