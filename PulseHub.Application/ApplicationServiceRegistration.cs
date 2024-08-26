using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PulseHub.Application.Common.Behaviors;

namespace PulseHub.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly);

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly);

            // Pipelines Behaviors
            options.AddOpenBehavior(typeof(RequestValidationPipelineBehavior<,>));
            options.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            options.AddOpenBehavior(typeof(RequestPerformancePipelineBehavior<,>));
            options.AddOpenBehavior(typeof(RequestExceptionHandlingPipelineBehavior<,>));
        });

        return services;
    }
}
