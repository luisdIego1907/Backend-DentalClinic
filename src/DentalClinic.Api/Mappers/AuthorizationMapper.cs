using DentalClinic.Api.Models.Requests;
using DentalClinic.Api.Models.Responses;
using DentalClinic.Dto;

namespace DentalClinic.Api.Mappers;

public static class AuthorizationMapper
{
    public static AuthorizationRequestDto ToDto(this AuthorizationRequestModel model)
    {
        return new AuthorizationRequestDto
        {
            username = model.username,
            password = model.password,
        };
    }


    public static AuthorizationResponse ToResponse(this AuthorizationResponseDto dto)
    {
        return new AuthorizationResponse
        {
            BearerToken = dto.BearerToken,
            ExpiresIn = dto.ExpiresIn,
        };
    }
}
