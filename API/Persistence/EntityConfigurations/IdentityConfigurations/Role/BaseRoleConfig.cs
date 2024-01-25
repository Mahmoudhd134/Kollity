using Domain.Identity.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations.IdentityConfigurations.Role;

public class BaseRoleConfig : IEntityTypeConfiguration<BaseRole>
{
    public void Configure(EntityTypeBuilder<BaseRole> builder)
    {
        builder.ToTable("Role");
    }
}