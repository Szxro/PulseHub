using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using PulseHub.Api.Scheme.Defaults;
using PulseHub.Api.Scheme.Handler;
using PulseHub.Api.Scheme.Options;
using PulseHub.Api.Scheme.Events;

namespace PulseHub.Api.Extensions;

public static partial class ApiExtensions
{
    public static AuthenticationBuilder AddAccessKeyAuth(this AuthenticationBuilder builder)
    {
        builder.AddScheme<AccessKeyAuthenticationSchemeOptions, AccessKeyAuthenticationHandler>(AccessKeyDefaults.AuthenticationScheme, options => 
        {
            options.Events = new AccessKeyEvents
            {
                OnHeaderReceivedContext = context =>
                {
                    if (context.HttpContext.Request.Headers.TryGetValue("AccessKey",out StringValues accessKey) 
                        && context.HttpContext.Request.Headers.TryGetValue("ApplicationName", out StringValues applicationName))
                    {
                        context.AccessKey = accessKey;
                        context.ApplicationName = applicationName;
                    }

                    return Task.CompletedTask;
                }
            };
        });

        return builder; 
    }
}
