using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Infrastructure.Options.Jwt;

public class JwtOptions : IOptionSetup
{
    public string SectionName => "JwtOptions";

    public bool ValidateAudience { get; set; }

    public bool ValidateIssuer { get; set; }

    public bool ValidateLifetime { get; set; }

    public bool ValidateIssuerSigningKey { get; set; }

    public string ValidIssuer { get; set; } = string.Empty;

    public string ValidAudience { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;
}
