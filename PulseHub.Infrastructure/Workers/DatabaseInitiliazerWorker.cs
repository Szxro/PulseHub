using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;

namespace PulseHub.Infrastructure.Workers;

public sealed class DatabaseInitiliazerWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DatabaseInitiliazerWorker> _logger;

    public DatabaseInitiliazerWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<DatabaseInitiliazerWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Worker {workerName} start at {date} with {time}",
            nameof(DatabaseInitiliazerWorker),
            DateTime.Now.ToShortDateString(),
            DateTime.Now.ToShortTimeString());

        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.Register(() =>
        {
            _logger.LogInformation(
                "Worker {workerName} was signal to stop at {date} with {time}",
                nameof(DatabaseInitiliazerWorker),
                DateTime.Now.ToShortDateString(),
                DateTime.Now.ToShortTimeString());
        });

        using IServiceScope scope = _scopeFactory.CreateScope();

        IDatabaseInitializerService initializerService = scope.ServiceProvider.GetRequiredService<IDatabaseInitializerService>();

        await initializerService.CanConnectAsync();

        await initializerService.MigrateAsync();
    }
}
