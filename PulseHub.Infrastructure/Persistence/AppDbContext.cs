using Microsoft.EntityFrameworkCore;
using PulseHub.Domain.Entities;
using PulseHub.Infrastructure.Extensions;
using PulseHub.SharedKernel.Contracts;
using DomainApplication = PulseHub.Domain.Entities.Application;

namespace PulseHub.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveColumnType("datetime2");

        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.ApplyGlobalQueryFilter<ISoftDelete>(entity => !entity.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> User => Set<User>();

    public DbSet<Credentials> Credentials => Set<Credentials>();

    public DbSet<DomainApplication> Application => Set<DomainApplication>();

    public DbSet<Provider> Provider => Set<Provider>();

    public DbSet<AccessKey> AccessKey => Set<AccessKey>();

    public DbSet<EmailCode> EmailCode => Set<EmailCode>();  

    public DbSet<RefreshToken> RefreshToken => Set<RefreshToken>();
}
