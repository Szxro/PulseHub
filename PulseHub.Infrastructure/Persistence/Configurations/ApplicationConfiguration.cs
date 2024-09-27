using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainApplication = PulseHub.Domain.Entities.Application;

namespace PulseHub.Infrastructure.Persistence.Configurations;

public class ApplicationConfiguration : IEntityTypeConfiguration<DomainApplication>
{
    public void Configure(EntityTypeBuilder<DomainApplication> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasIndex(x => x.ProviderApplicationId).IsUnique();
    }
}
