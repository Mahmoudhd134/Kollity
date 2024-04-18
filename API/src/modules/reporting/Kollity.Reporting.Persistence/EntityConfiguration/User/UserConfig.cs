using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.User;

public class UserConfig : IEntityTypeConfiguration<Domain.UserModels.User>
{
    public void Configure(EntityTypeBuilder<Domain.UserModels.User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasDiscriminator(x => x.Type);
        builder.Property(u => u.UserName).HasMaxLength(127);
        builder.Property(u => u.Email).HasMaxLength(255);
        builder.Property(u => u.ProfileImage).HasMaxLength(511);
        builder.Property(s => s.FullNameInArabic).HasMaxLength(255);
        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.ToTable("User");
    }
}