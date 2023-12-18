using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BaseRoleConfig : IEntityTypeConfiguration<BaseRole>
{
    public void Configure(EntityTypeBuilder<BaseRole> builder)
    {
        builder.ToTable("Roles");
    }
}