using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;

namespace PulseHub.Infrastructure.Services;

public class UserConnectionTrackerService : IUserConnectionTrackerService
{
    private readonly ILogger<UserConnectionTrackerService> _logger;

    private readonly Dictionary<string,string> _storage = new Dictionary<string,string>();

    private readonly static object _lock = new object();

    public UserConnectionTrackerService(ILogger<UserConnectionTrackerService> logger)
    {
        _logger = logger;
    }

    public void AddConnection(string currentUser, string connectionId)
    {
        bool isLockTaken = false; //LockTaken pattern

        try
        {
            Monitor.Enter(_lock,ref isLockTaken); // To avoid race conditions

            // Critical zone
            if (_storage.ContainsKey(currentUser))
            {
                _logger.LogWarning(
                    "The connection tracker storage have already register the provide user {currentUser}!!!.",
                    currentUser);

                return;
            }

            _storage.Add(currentUser, connectionId);

        } finally
        {
            if (isLockTaken)
            {
                Monitor.Exit(_lock);
            }
        }
    }

    public void RemoveConnectionByUser(string currentUser)
    {
        bool isLockTaken = false;

        try
        {
            Monitor.Enter(_lock, ref isLockTaken);

            //Critical zone
            if (!_storage.ContainsKey(currentUser))
            {
                _logger.LogWarning(
                    "The connection tracker storage don't have the provide user {currentUser}!!!.",
                    currentUser);

                return;
            }

            _storage.Remove(currentUser);

            _logger.LogInformation("User {currentUser} has been removed from the connection tracker.", currentUser);

        } finally
        {
            if (isLockTaken)
            {
                Monitor.Exit(_lock);
            }
        }
    }

    public string? GetConnectionByUser(string currentUser)
    {
        if (_storage.TryGetValue(currentUser, out string? connectionId))
        {
            return connectionId;
        }

        _logger.LogWarning("Connection not found for user {currentUser}.",
                           currentUser);

        return null;
    }

    public Dictionary<string, string> GetAllConnections()
    {
        // returning a copy of the current storage dictionary
        return new Dictionary<string, string>(_storage);
    }
}
