using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using PulseHub.Domain.Contracts;

namespace PulseHub.Api.Filters;

public class ConnectionTrackerHubFilter : IHubFilter
{
    private readonly ILogger<ConnectionTrackerHubFilter> _logger;
    private readonly IUserConnectionTrackerService _trackerService;

    public ConnectionTrackerHubFilter(
        ILogger<ConnectionTrackerHubFilter> logger,
        IUserConnectionTrackerService trackerService)
    {
        _logger = logger;
        _trackerService = trackerService;
    }

    public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        Task response = next(context);

        (string? currentUser, string? currentConnectionId) = GetCurrentUserAndConnectionId(context);

        EnsureUserAndConnectionIdValid(currentUser, currentConnectionId);

        _trackerService.AddConnection(currentUser,currentConnectionId);

        _logger.LogInformation("New connection added to the storage with the user {User} and connectionId {connectionId}",
                               currentUser,
                               currentConnectionId);
        return response;
    }

    public Task OnDisconnectedAsync(HubLifetimeContext context, Exception? exception, Func<HubLifetimeContext, Exception?, Task> next)
    {
        Task response = next(context, exception);

        (string? currentUser, string? currentConnectionId) = GetCurrentUserAndConnectionId(context);

        EnsureUserAndConnectionIdValid(currentUser, currentConnectionId);

        if (exception is not null)
        {
            _logger.LogWarning(
                "The current connection was possibly dropped due to an exception for the user: {user} with the error message: {message}",
                currentUser,
                exception.Message);
        }

        _trackerService.RemoveConnectionByUser(currentUser);

        _logger.LogInformation("The connection was remove from the storage with the user {User} and connectionId {connectionId}",
                               currentUser,
                               currentConnectionId);
        return response;
    }

    private void EnsureUserAndConnectionIdValid([NotNull] string? currentUser, [NotNull] string? connectionId)
    {
        if (string.IsNullOrEmpty(currentUser) || string.IsNullOrEmpty(connectionId))
        {
            _logger.LogWarning("The current user or connection Id is missing. User: {User}, Connection ID: {ConnectionId}",
                              currentUser ?? "Unknown",
                              connectionId ?? "Unknown");

            throw new HubException("The current user or connection Id is invalid.");
        }
    }

    private (string? currentUser, string? currentConnectionId) GetCurrentUserAndConnectionId(HubLifetimeContext context)
    {
        string? currentUser = context.Context.User?.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;

        string? currentConnectionId = context.Context.ConnectionId;

        return (currentUser, currentConnectionId);
    }
}