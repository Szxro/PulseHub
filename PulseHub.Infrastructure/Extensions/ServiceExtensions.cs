using Microsoft.Extensions.DependencyInjection;
using PulseHub.Domain.Contracts;
using PulseHub.Infrastructure.Common;
using PulseHub.Infrastructure.Services;

namespace PulseHub.Infrastructure.Extensions;

public static partial class InfrastructureExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Memory Cache is designed to be use as singleton
        services.AddSingleton<ICacheService, CacheService>();

        services.AddSingleton<IUserConnectionTrackerService, UserConnectionTrackerService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IDatabaseInitializerService, DatabaseInitializerService>();

        // We are working with the current http context (need to be scoped)
        services.AddScoped<ICurrentUserService, CurrentUserService>(); 

        services.AddTransient<IHashingService, HashingService>();

        services.AddTransient<IDomainEventDispatcherService, DomainEventDispatcherService>();

        services.AddTransient<IEmailService, EmailService>();

        services.AddTransient<IAccessKeyService, AccessKeyService>();

        services.AddTransient<ITokenService, TokenService>();

        services.AddTransient<IMediaCompressionService, MediaCompressionService>();

        return services;
    }
}
