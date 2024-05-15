using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.User.API.Data.IdentityConfigurations.Role;

public class BaseRoleConfig : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.NormalizedName).IsUnique();

        builder.HasData([
            new IdentityRole<Guid>
            {
                Id = new Guid("be2a5cab-0ae7-4335-8316-4154a5cfa35f"),
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole<Guid>
            {
                Id = new Guid("126abefb-6d50-4d58-9419-c8e1f39a01d8"),
                Name = "Doctor",
                NormalizedName = "DOCTOR"
            },
            new IdentityRole<Guid>
            {
                Id = new Guid("bf9c94d0-ca32-4b64-aa5a-3c03b44db740"),
                Name = "Student",
                NormalizedName = "STUDENT"
            },
            new IdentityRole<Guid>
            {
                Id = new Guid("6ddc2275-7ae1-40ca-9f6f-c5b5c637c5d8"),
                Name = "Assistant",
                NormalizedName = "ASSISTANT"
            }
        ]);

        builder.ToTable("Role");
    }
}