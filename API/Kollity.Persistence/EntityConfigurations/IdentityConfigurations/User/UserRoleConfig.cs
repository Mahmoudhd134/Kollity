using Kollity.Domain.Identity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Persistence.EntityConfigurations.IdentityConfigurations.User;

public class UserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder
            .HasOne<BaseUser>()
            .WithMany(x => x.Roles)
            .HasForeignKey(x => x.UserId);
        builder.ToTable("UserRole");
    }
}