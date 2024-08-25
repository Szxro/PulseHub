using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Infrastructure.Options.Database;

public class DatabaseOptions : IOptionSetup
{
    public string SectionName => "DatabaseOptions";

    public string ConnectionString { get; set; } = string.Empty;

    public int CommandTimeout { get; set; }

    public bool EnableDetailedErrors { get; set; }

    public bool EnableSensitiveDataLogging { get; set; }
}
