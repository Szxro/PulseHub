using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace PulseHub.Api.Filters;

public class GlobalLoggingHubFilter : IHubFilter
{
    private readonly ILogger _logger;

    public GlobalLoggingHubFilter(ILogger logger)
    {
        _logger = logger;
    }

    public async ValueTask<object?> InvokeMethodAsync(
        HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object?>> next)
    {
        string currentUser = invocationContext.Context.User?.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value ?? "Non-Kown user";

        try
        {
 
            _logger.LogInformation("The user {username} is calling a hub method : {hubMethod} with arguments : {arguments}",
                                   currentUser,
                                   invocationContext.HubMethodName,
                                   invocationContext.HubMethodArguments);

            return await next(invocationContext);

        } catch (Exception ex)
        {
            _logger.LogError("An exception ocurred when the user {username} was calling {hubMethodName}: {message}",
                             currentUser,
                             invocationContext.HubMethodName,
                             ex.Message);

            throw;
        }
    }
}
