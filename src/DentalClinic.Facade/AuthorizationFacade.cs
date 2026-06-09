using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DentalClinic.Domain.Entities;
using DentalClinic.DomainService;
using DentalClinic.Dto;
using DentalClinic.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DentalClinic.Facade;

public class AuthorizationFacade : IAuthorizationFacade
{
    private readonly IUserService userService;
    private readonly IConfiguration configuration;

    public AuthorizationFacade(IUserService userService, IConfiguration configuration)
    {
        this.userService = userService;
        this.configuration = configuration;
    }
    public async Task<AuthorizationResponseDto> AuthorizeAsync(AuthorizationRequestDto request)
    {
        var user = await userService.GetByUserAndPassword(request).ConfigureAwait(false);

        if (user == null)
        {
            throw new UnauthorizedResponseException();
        }

        var jwtSettings = configuration.GetSection("Jwt");
        var expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"]);

        var token = GenerateJwtToken(user, jwtSettings, expirationMinutes);

        return new AuthorizationResponseDto
        {
            BearerToken = token,
            ExpiresIn = DateTime.UtcNow.AddMinutes(expirationMinutes),
        };
    }

    private string GenerateJwtToken(User user, IConfigurationSection jwtSettings, int expirationMinutes)
    {
        var secret = jwtSettings["Secret"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.user_id.ToString()),
            new(ClaimTypes.Name, user.username),
            new("externalId", user.user_resource_id.ToString()),
        };

        claims.AddRange([.. user.UserRoles.Select(r => new Claim(ClaimTypes.Role, r.role.Name))]); // grabs all roles assigned to a user and adds them as claims

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
