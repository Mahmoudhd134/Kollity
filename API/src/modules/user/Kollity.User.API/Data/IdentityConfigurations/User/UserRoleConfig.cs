using Kollity.User.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.User.API.Data.IdentityConfigurations.User;

public class UserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        // builder
        //     .HasOne<BaseUser>()
        //     .WithMany(x => x.Roles)
        //     .HasForeignKey(x => x.UserId);

        // builder.HasData([
        //     new IdentityUserRole<Guid>
        //     {
        //         UserId = new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa"),
        //         RoleId = new Guid("be2a5cab-0ae7-4335-8316-4154a5cfa35f")
        //     }
        // ]);

        builder.ToTable("UserRole");
    }
}