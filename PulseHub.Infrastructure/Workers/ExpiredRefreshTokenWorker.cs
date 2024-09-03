using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PulseHub.Domain.Entities;
using PulseHub.Infrastructure.Strategies.Context;

namespace PulseHub.Infrastructure.Workers;

public class ExpiredRefreshTokenWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    private static readonly TimeSpan Timeout = TimeSpan.FromMinutes(50);

    public ExpiredRefreshTokenWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(Timeout);

        while (await timer.WaitForNextTickAsync(stoppingToken)
            && !stoppingToken.IsCancellationRequested)
         {
            using IServiceScope scope = _scopeFactory.CreateScope();

            ExpirationStrategyContext<RefreshToken> context = scope.ServiceProvider.GetRequiredService<ExpirationStrategyContext<RefreshToken>>();

            await context.ExecuteAsync(DateTime.Now,stoppingToken);
        }
    }
}
