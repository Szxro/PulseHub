using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PulseHub.Domain.Entities;

namespace PulseHub.Infrastructure.Persistence.Configurations;

public class AccessKeyConfiguration : IEntityTypeConfiguration<AccessKey>
{
    public void Configure(EntityTypeBuilder<AccessKey> builder)
    {
        builder.HasIndex(x => x.EncryptedKey).IsUnique();
    }
}
