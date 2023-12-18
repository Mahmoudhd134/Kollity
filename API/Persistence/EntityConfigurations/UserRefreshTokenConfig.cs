using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserRefreshTokenConfig : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserId).HasMaxLength(450);
        builder.Property(u => u.UserAgent).HasMaxLength(511);
        builder.Property(u => u.RefreshToken).HasMaxLength(127);

        builder
            .HasOne(u => u.User)
            .WithMany(u => u.UserRefreshTokens)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(u => u.UserId);
        builder.HasIndex(u => new { u.UserId, u.UserAgent }).IsUnique();

        builder.ToTable("UserRefreshTokens");
    }
}