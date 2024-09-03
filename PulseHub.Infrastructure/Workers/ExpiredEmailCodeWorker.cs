using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PulseHub.Domain.Entities;
using PulseHub.Infrastructure.Strategies.Context;

namespace PulseHub.Infrastructure.Workers;

public class ExpiredEmailCodeWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private static readonly TimeSpan Timeout = TimeSpan.FromMinutes(35);

    public ExpiredEmailCodeWorker(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(Timeout);

        while (await timer.WaitForNextTickAsync(stoppingToken)
            && !stoppingToken.IsCancellationRequested)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            ExpirationStrategyContext<EmailCode> strategyContext = scope.ServiceProvider.GetRequiredService<ExpirationStrategyContext<EmailCode>>();

            await strategyContext.ExecuteAsync(DateTime.Now, stoppingToken);
        }
    }
}
