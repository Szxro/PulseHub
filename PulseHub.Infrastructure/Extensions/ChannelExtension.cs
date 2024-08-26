using Microsoft.Extensions.DependencyInjection;
using PulseHub.Infrastructure.Channels;

namespace PulseHub.Infrastructure.Extensions;

public static partial class InfrastructureExtensions
{
    public static IServiceCollection AddChannels(this IServiceCollection services)
    {
        // Need to be singleton to maintain state
        services.AddSingleton<DomainEventChannel>();

        return services;
    }
}
