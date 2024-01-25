using Domain.AssistantModels;
using Domain.Identity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations.AssistantConfigurations;

public class AssistantConfig : IEntityTypeConfiguration<Assistant>
{
    public void Configure(EntityTypeBuilder<Assistant> builder)
    {
    }
}