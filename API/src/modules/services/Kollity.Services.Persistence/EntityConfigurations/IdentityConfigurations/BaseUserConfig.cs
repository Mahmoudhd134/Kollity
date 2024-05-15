using Kollity.Services.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Services.Persistence.EntityConfigurations.IdentityConfigurations;

public class BaseUserConfig : IEntityTypeConfiguration<BaseUser>
{
    public void Configure(EntityTypeBuilder<BaseUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasDiscriminator(x => x.Type);
        builder.Property(u => u.ProfileImage).HasMaxLength(255);
        builder.Property(s => s.FullNameInArabic).HasMaxLength(127);
        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();
        builder.ToTable("User");
    }
}