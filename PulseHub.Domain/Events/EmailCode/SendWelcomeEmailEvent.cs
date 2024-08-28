using PulseHub.SharedKernel;

namespace PulseHub.Domain.Events.EmailCode;

public class SendWelcomeEmailEvent : DomainEvent
{
    public SendWelcomeEmailEvent(string username,string email)
    {
        Username = username;
        Email = email;
    }

    public string Username { get; } = string.Empty;

    public string Email { get; } = string.Empty;
}
