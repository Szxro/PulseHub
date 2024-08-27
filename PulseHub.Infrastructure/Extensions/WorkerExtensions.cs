using Microsoft.Extensions.DependencyInjection;
using PulseHub.Infrastructure.Workers;

namespace PulseHub.Infrastructure.Extensions;

public static partial class InfrastructureExtensions
{
    public static IServiceCollection AddWorkers(this IServiceCollection services)
    {
        services.AddHostedService<DatabaseInitliazerWorker>();

        services.AddHostedService<DomainEventDispatcher>();

        return services;
    }
}
