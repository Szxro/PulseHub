namespace PulseHub.Domain.Contracts;

public interface IDatabaseInitializerService
{
    Task CanConnectAsync(CancellationToken cancellationToken = default);

    Task MigrateAsync(CancellationToken cancellationToken = default);

    Task SeedAsync(CancellationToken cancellationToken = default);
}
