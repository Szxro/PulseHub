using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;

namespace PulseHub.Infrastructure.Persistence.Interceptors;

public class EnforceEmailCodeInterceptor : SaveChangesInterceptor
{
    private readonly IEmailService _emailService;

    public EnforceEmailCodeInterceptor(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            EnforceEmailCode(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void EnforceEmailCode(DbContext dbContext)
    {
        List<User> users = dbContext.ChangeTracker.Entries<User>()
                                                  .Where(x => x.State == EntityState.Added)
                                                  .Select(x => x.Entity)
                                                  .ToList();
        if (!users.Any()) return;

        foreach (User user in users)
        {
            user.AddEmailCode(new EmailCode
            {
                Code = _emailService.GenerateCode()
            });
        }
    }
}
