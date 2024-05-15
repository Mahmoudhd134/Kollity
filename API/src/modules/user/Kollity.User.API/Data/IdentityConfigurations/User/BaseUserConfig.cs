using Kollity.User.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.User.API.Data.IdentityConfigurations.User;

public class BaseUserConfig : IEntityTypeConfiguration<BaseUser>
{
    public void Configure(EntityTypeBuilder<BaseUser> builder)
    {
        builder.Property(u => u.ProfileImage).HasMaxLength(255);
        builder.Property(u => u.PhoneNumber).HasMaxLength(127);

        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();


        builder.HasData([
            new BaseUser()
            {
                Id = new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa"),
                UserName = "Mahmoudhd134",
                NormalizedUserName = "MAHMOUDHD134",
                Email = "nassermahmoud571@gmail.com",
                NormalizedEmail = "NASSERMAHMOUD571@GMAIL.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEPRFyxksWTOaY3gzYwnqUGS8FT0q1kCjlaUo1KP/Uu3R1seoxDWoi1tlyw8Uc69YNA==",
                SecurityStamp = "6TPMB3KY7R4NAIGXTMKLOWGRE2HQOOBY",
                ConcurrencyStamp = "a443bf96-da75-4046-8452-7d64553b4533",
                LockoutEnabled = true,
                AccessFailedCount = 0
            }
        ]);

        builder.ToTable("User");
    }
}