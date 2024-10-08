﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseHub.Domain.Entities;

namespace PulseHub.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.Username).IsUnique();

        builder.OwnsOne(x => x.Email, emailOptions =>
        {
            emailOptions.HasIndex(x => x.Value).IsUnique();

            emailOptions.Property(x => x.Value).HasColumnName("email");
        });
    }
}
