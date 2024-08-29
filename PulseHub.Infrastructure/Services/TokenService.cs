using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Infrastructure.Options.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PulseHub.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;

    private static readonly string Algorithm = SecurityAlgorithms.HmacSha256;
    private readonly ILogger<TokenService> _logger;

    public TokenService(
        IOptions<JwtOptions> options,
        ILogger<TokenService> logger)
    {
        _jwtOptions = options.Value;
        _logger = logger;
    }
    public string GenerateToken(User user,double lifeTime = 10)
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        try
        {
            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),Algorithm);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                _jwtOptions.ValidIssuer,
                _jwtOptions.ValidAudience,
                GenerateClaims(user),
                DateTime.Now,
                DateTime.Now.AddMinutes(lifeTime),
                credentials);

            string token = handler.WriteToken(securityToken);

            return token;

        } catch (Exception ex)
        {
            _logger.LogError(
                "An unexpected error happen while trying to create a token for a user with the error message : '{message}'",
                ex.Message);

            throw;
        }
    }
    public string GenerateRefreshToken(int length = 32)
    {
        byte[] buffer = new byte[length];

        using RandomNumberGenerator rng = RandomNumberGenerator.Create();

        rng.GetBytes(buffer);

        // Converting the buffer to its representation in base 64 string
        string refreshToken = Convert.ToBase64String(buffer);   

        return refreshToken;
    }

    private IEnumerable<Claim> GenerateClaims(User user)
    {
        Claim[] claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name,user.Username),
            new Claim(JwtRegisteredClaimNames.Email,user.Email.Value),
            new Claim(JwtRegisteredClaimNames.Iat,DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64)
        };

        return claims;
    }

}
