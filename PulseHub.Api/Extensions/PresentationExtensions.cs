using Microsoft.AspNetCore.Authentication.JwtBearer;

using PulseHub.Api.Middlewares;

namespace PulseHub.Api.Extensions;

public static partial class ApiExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        services.AddAuthorization();

        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.AddSwaggerGenWithAuth();

        // Handlers
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
