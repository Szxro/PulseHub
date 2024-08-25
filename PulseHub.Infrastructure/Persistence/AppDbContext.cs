using Microsoft.EntityFrameworkCore;
using PulseHub.Domain.Entities;

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

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> User => Set<User>();

    public DbSet<Credentials> Credentials => Set<Credentials>();

    public DbSet<UserCredentials> UserCredentials => Set<UserCredentials>();

    public DbSet<Application> Application => Set<Application>();

    public DbSet<Provider> Provider => Set<Provider>();

    public DbSet<AccessKey> AccessKey => Set<AccessKey>();
}
