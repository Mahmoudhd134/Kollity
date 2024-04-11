using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.User.API.Data.IdentityConfigurations.UserRefreshToken;

public class UserRefreshTokenConfig : IEntityTypeConfiguration<Models.UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<Models.UserRefreshToken> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserId).HasMaxLength(450);
        builder.Property(u => u.DeviceToken).HasMaxLength(1023);
        builder.Property(u => u.RefreshToken).HasMaxLength(127);

        builder
            .HasOne(u => u.User)
            .WithMany(u => u.UserRefreshTokens)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(u => u.UserId);
        builder.HasIndex(u => new { u.UserId, u.DeviceToken }).IsUnique();

        builder.ToTable("UserRefreshToken");
    }
}