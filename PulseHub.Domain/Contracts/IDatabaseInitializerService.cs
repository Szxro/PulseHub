namespace PulseHub.Domain.Contracts;

public interface IDatabaseInitializerService
{
    Task CanConnectAsync();

    Task MigrateAsync();
}
