using Microsoft.Extensions.DependencyInjection;
using PulseHub.Infrastructure.Persistence.Interceptors;

namespace PulseHub.Infrastructure.Extensions;

public static partial class InfrastructureExtensions
{
    public static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        services.AddSingleton<AuditableEntityInterceptor>();

        services.AddSingleton<DomainEventInterceptor>();

        return services;
    }
}
