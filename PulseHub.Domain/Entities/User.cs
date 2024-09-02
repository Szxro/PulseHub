using PulseHub.Domain.ValueObjects;
using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class User : Entity
{
    public User()
    {
        Applications = new HashSet<Application>();
        Credentials = new HashSet<Credentials>();
        EmailCodes = new HashSet<EmailCode>();
        RefreshTokens = new HashSet<RefreshToken>();
    }

    public string FirstName { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public Email Email { get; set; } = null!;

    public bool LockOutEnable { get; set; } = true;

    public DateTime LockOutEnd { get; set; } = DateTime.MinValue;

    public int AccessFailedCount { get; set; } = 0;

    public ICollection<Credentials> Credentials { get; set; }

    public ICollection<Application> Applications { get; set; }

    public ICollection<EmailCode> EmailCodes { get; }

    public ICollection<RefreshToken> RefreshTokens { get; }

    public void AddCredentials(Credentials credentials)
    {
        Credentials.Add(credentials);
    }

    public void AddEmailCode(EmailCode emailCode)
    {
        EmailCodes.Add(emailCode);  
    }

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshTokens.Add(refreshToken);
    }
}
