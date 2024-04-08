using Kollity.Services.Domain.AssignmentModels.AssignmentGroupModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Services.Persistence.EntityConfigurations.AssignmentConfigurations.AssignmentGroupModels;

public class AssignmentGroupConfig : IEntityTypeConfiguration<AssignmentGroup>
{
    public void Configure(EntityTypeBuilder<AssignmentGroup> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).ValueGeneratedOnAdd().IsRequired();

        builder
            .HasOne(x => x.Room)
            .WithMany(x => x.AssignmentGroups)
            .HasForeignKey(x => x.RoomId);

        builder.HasIndex(x => x.Code).IsUnique();

        builder.ToTable("AssignmentGroup");
    }
}