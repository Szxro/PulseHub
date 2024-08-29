using Microsoft.Extensions.Configuration;
using PulseHub.Infrastructure.Options.Base;

namespace PulseHub.Infrastructure.Options.Jwt;

public class JwtOptionsSetup : BaseSetup<JwtOptions>
{
    public JwtOptionsSetup(IConfiguration configuration) : base(configuration) { }
}
