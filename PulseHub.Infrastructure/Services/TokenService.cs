using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Infrastructure.Options.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PulseHub.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;
    private readonly ILogger<TokenService> _logger;

    private static readonly string Algorithm = SecurityAlgorithms.HmacSha256;

    private readonly TokenValidationParameters _validationParameters;

    public TokenService(
        IOptions<JwtOptions> options,
        ILogger<TokenService> logger)
    {
        _jwtOptions = options.Value;
        _logger = logger;

        _validationParameters = new TokenValidationParameters
        {
            ValidateAudience = _jwtOptions.ValidateAudience,
            ValidateIssuer = _jwtOptions.ValidateIssuer,
            ValidateLifetime = false, // to not have a lifetime exception
            ValidateIssuerSigningKey = _jwtOptions.ValidateIssuerSigningKey,
            ValidIssuer = _jwtOptions.ValidIssuer,
            ValidAudience = _jwtOptions.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
        };
    }
    public string GenerateToken(User user,double lifeTime = 10)
    {
        // Its more faster than JwtSecurityTokenHandler 
        JsonWebTokenHandler handler = new JsonWebTokenHandler();

        try
        {
            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),Algorithm);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.Now.AddMinutes(lifeTime),
                SigningCredentials = credentials,
                Issuer = _jwtOptions.ValidIssuer,
                Audience = _jwtOptions.ValidAudience
            };

            string token = handler.CreateToken(tokenDescriptor);

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

    private ClaimsIdentity GenerateClaims(User user)
    {
        ClaimsIdentity claims = new ClaimsIdentity(
            new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name,user.Username),
            new Claim(JwtRegisteredClaimNames.Email,user.Email.Value),
        });

        return claims;
    }

    public async Task<(bool isValid,ClaimsIdentity? claims)> ValidateToken(string token)
    {
        JsonWebTokenHandler handler = new JsonWebTokenHandler();

        TokenValidationResult result = await handler.ValidateTokenAsync(token,_validationParameters);

        if (result.Exception is not null)
        {
            return (false,null);
        }

        return (true,result.ClaimsIdentity);
    }
}
