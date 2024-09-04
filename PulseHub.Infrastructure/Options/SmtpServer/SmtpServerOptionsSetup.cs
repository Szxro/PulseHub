using Microsoft.Extensions.Configuration;
using PulseHub.Infrastructure.Options.Base;

namespace PulseHub.Infrastructure.Options.SmtpServer;

public class SmtpServerOptionsSetup : BaseOptionSetup<SmtpServerOptions>
{
    public SmtpServerOptionsSetup(IConfiguration configuration) : base(configuration) { }
}
