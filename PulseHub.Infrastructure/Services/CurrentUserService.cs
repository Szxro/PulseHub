using Microsoft.AspNetCore.Http;
using PulseHub.Domain.Contracts;
using System.IdentityModel.Tokens.Jwt;

namespace PulseHub.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContext;

    public CurrentUserService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public string? GetCurrentUser()
    {
        return _httpContext.HttpContext?.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Name)?.Value;
    }
}
