using Microsoft.Extensions.DependencyInjection;
using PulseHub.Infrastructure.Workers;

namespace PulseHub.Infrastructure.Extensions;

public static partial class InfrastructureExtensions
{
    public static IServiceCollection AddWorkers(this IServiceCollection services)
    {
        services.AddHostedService<DatabaseInitiliazerWorker>();

        services.AddHostedService<DomainEventDispatcher>();

        services.AddHostedService<ExpiredEmailCodeWorker>();

        services.AddHostedService<ExpiredRefreshTokenWorker>();

        return services;
    }
}
