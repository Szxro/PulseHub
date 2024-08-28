using Microsoft.EntityFrameworkCore;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Infrastructure.Common;
using PulseHub.Infrastructure.Persistence;

namespace PulseHub.Infrastructure.Repositories;

public class EmailCodeRepository : GenericRepository<EmailCode>, IEmailCodeRepository
{
    public EmailCodeRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<string?> GetEmailCodeByUsernameAndEmail(string username, string email,CancellationToken cancellationToken = default)
    {
        return await _dbContext.EmailCode.AsNoTracking()
                                         .Include(x => x.User)
                                         .Where(x => x.User.Username == username && x.User.Email.Value == email)
                                         .Select(x => x.Code)
                                         .FirstOrDefaultAsync(cancellationToken);   
    }
}
