using Microsoft.Extensions.Configuration;
using PulseHub.Infrastructure.Options.Base;

namespace PulseHub.Infrastructure.Options.Database;

public class DatabaseOptionsSetup : BaseOptionSetup<DatabaseOptions>
{
    public DatabaseOptionsSetup(IConfiguration configuration) : base(configuration) { }
}
