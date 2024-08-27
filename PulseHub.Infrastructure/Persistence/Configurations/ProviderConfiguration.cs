using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseHub.Domain.Entities;

namespace PulseHub.Infrastructure.Persistence.Configurations;

public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    private const string DiscordDescription = "Discord is a platform for text, voice, and video chat, designed for creating and managing communities and staying connected.";

    private const string TelegramDescription = "Telegram is a messaging app that offers fast, secure text, voice, and video communication. It supports group chats, channels, and multimedia sharing.";

    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.HasData(
            new Provider
            {
                Id = 1,
                Name = "Discord",
                Description = DiscordDescription,
                CreatedAtUtc = DateTime.UtcNow,
                ModifiedAtUtc = DateTime.UtcNow,
            },
            new Provider
            {
                Id = 2,
                Name = "Telegram",
                Description = TelegramDescription,
                CreatedAtUtc = DateTime.UtcNow,
                ModifiedAtUtc = DateTime.UtcNow,
            }
         );
    }
}
