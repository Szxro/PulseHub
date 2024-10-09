using Microsoft.AspNetCore.SignalR;
using PulseHub.Api.Filters;
using PulseHub.Api.Middlewares;

namespace PulseHub.Api.Extensions;

public static partial class ApiExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IHostEnvironment environment)
    {
        services.AddAuthentication().AddAccessKeyAuth().AddJwtBearer();
        services.AddAuthorization();
        services.AddSignalR(options =>
        {
            //Development options
            if (environment.IsDevelopment())
            {
                options.EnableDetailedErrors = true;
            }

            // Global options
            options.DisableImplicitFromServicesParameters = true;


            // Hub Filters

            //Transient
            options.AddFilter<ConnectionTrackerHubFilter>();

            // Singleton
            options.AddFilter(new InvocationLoggingHubFilter());
        });
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.AddSwaggerGenWithAuth();

        // Handlers
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();


        services.AddCors(options => 
        {
            options.AddPolicy("default", policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
                //policy.AllowCredentials(); (need to allow credentials when using signalR and need to define an origin)
                policy.SetIsOriginAllowed(origin => true);
            });
        });

        return services;
    }
}
