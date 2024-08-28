using Microsoft.Extensions.Configuration;
using PulseHub.Infrastructure.Options.Base;

namespace PulseHub.Infrastructure.Options.SmtpServer;

public class SmtpServerOptionsSetup : BaseSetup<SmtpServerOptions>
{
    public SmtpServerOptionsSetup(IConfiguration configuration) : base(configuration) { }
}
