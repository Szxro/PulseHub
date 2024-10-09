namespace PulseHub.Domain.Contracts;

public interface IUserConnectionTrackerService
{
    void AddConnection(string currentUser,string connectionId);

    void RemoveConnectionByUser(string currentUser);

    string? GetConnectionByUser(string currentUser);

    Dictionary<string, string> GetAllConnections();
}
