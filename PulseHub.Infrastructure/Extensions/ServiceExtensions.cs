using Microsoft.Extensions.DependencyInjection;
using PulseHub.Domain.Contracts;
using PulseHub.Infrastructure.Common;
using PulseHub.Infrastructure.Services;

namespace PulseHub.Infrastructure.Extensions;

public static partial class InfrastructureExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IDatabaseInitializerService, DatabaseInitializerService>();

        services.AddScoped<ICurrentUserService, CurrentUserService>(); // We are working with the current http context (need to be scoped)

        services.AddTransient<IHashingService, HashingService>();

        services.AddTransient<IDomainEventDispatcherService, DomainEventDispatcherService>();

        services.AddTransient<IEmailService, EmailService>();

        services.AddTransient<IAccessKeyService, AccessKeyService>();

        services.AddTransient<ITokenService, TokenService>();

        return services;
    }
}
