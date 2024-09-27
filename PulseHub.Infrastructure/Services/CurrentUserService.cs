using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

using PulseHub.Domain.Contracts;

namespace PulseHub.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContext;

    public CurrentUserService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public (string? username, string? email) GetCurrentUser()
    {
        string? username = _httpContext?.HttpContext?.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Name)?.Value;

        string? email = _httpContext?.HttpContext?.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

        return (username, email);
    }
}
