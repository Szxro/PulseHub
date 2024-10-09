using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace PulseHub.Api.Filters;

public class InvocationLoggingHubFilter : IHubFilter
{
    public async ValueTask<object?> InvokeMethodAsync(
        HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object?>> next)
    {
        ILogger<InvocationLoggingHubFilter> logger = invocationContext.ServiceProvider.GetRequiredService<ILogger<InvocationLoggingHubFilter>>();

        string currentUser = invocationContext.Context.User?.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value ?? "Unknown user";

        Stopwatch stopwatch = Stopwatch.StartNew();

        try
        {
            logger.LogInformation("The user {username} is calling a hub method {hubMethod} with arguments {arguments}",
                                   currentUser,
                                   invocationContext.HubMethodName,
                                   invocationContext.HubMethodArguments);

            return await next(invocationContext);

        }
        catch (Exception ex)
        {
            logger.LogError("An exception ocurred when the user {username} was calling {hubMethodName} with the error message {message}",
                             currentUser,
                             invocationContext.HubMethodName,
                             ex.Message);
            throw;
        }
        finally
        {
            stopwatch.Stop();

            long elapsedTime = stopwatch.ElapsedMilliseconds;

            logger.LogInformation(
                "The current hub method {hubMethod} invoke by the user {user} completed in {elapsedTime}ms",
                invocationContext.HubMethodName,
                currentUser,
                elapsedTime);
        }
    }
}
