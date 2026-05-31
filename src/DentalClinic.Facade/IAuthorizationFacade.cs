using DentalClinic.Dto;

namespace DentalClinic.Facade;

public interface IAuthorizationFacade
{
    Task<AuthorizationResponseDto> AuthorizeAsync(AuthorizationRequestDto request);

}
