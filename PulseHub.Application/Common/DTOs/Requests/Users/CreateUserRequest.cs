namespace PulseHub.Application.Common.DTOs.Requests.Users;

public record CreateUserRequest(
    string firstName,
    string lastName,
    string username,
    string email,
    string password);
