﻿using Kollity.Domain.Identity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Persistence.EntityConfigurations.IdentityConfigurations.User;

public class BaseUserConfig : IEntityTypeConfiguration<BaseUser>
{
    public void Configure(EntityTypeBuilder<BaseUser> builder)
    {
        builder.HasDiscriminator(u => u.Type);

        builder.Property(u => u.ProfileImage).HasMaxLength(255);
        builder.Property(u => u.PhoneNumber).HasMaxLength(127);
        builder.Property(s => s.FullNameInArabic).HasMaxLength(127);

        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();
        builder.ToTable("User");
    }
}