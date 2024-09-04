using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PulseHub.Infrastructure.Options.Jwt;

public class JwtBearerOptionsSetup : IPostConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;

    public JwtBearerOptionsSetup(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }

    // These configuration run after all IConfigureOptions<TOptions> (singleton)
    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = _jwtOptions.ValidateAudience,
            ValidateIssuer = _jwtOptions.ValidateIssuer,
            ValidateLifetime = _jwtOptions.ValidateLifetime,
            ValidateIssuerSigningKey = _jwtOptions.ValidateIssuerSigningKey,
            ValidAudience = _jwtOptions.ValidAudience,
            ValidIssuer = _jwtOptions.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            ClockSkew = TimeSpan.Zero // must override (validating time) (default one is 5 min)
        };
    }
}
