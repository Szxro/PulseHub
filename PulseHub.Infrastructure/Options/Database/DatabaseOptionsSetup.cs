using Microsoft.Extensions.Configuration;
using PulseHub.Infrastructure.Options.Base;

namespace PulseHub.Infrastructure.Options.Database;

public class DatabaseOptionsSetup : BaseSetup<DatabaseOptions>
{
    public DatabaseOptionsSetup(IConfiguration configuration) : base(configuration) { }
}
