using PulseHub.SharedKernel;

namespace PulseHub.Domain.Events.Users;

public class UserLockOutEvent : DomainEvent
{
    public UserLockOutEvent(string username,string email)
    {
        Username = username;
        Email = email;  
    }

    public string Username { get; set; }

    public string Email { get; set; }
}
