using Domain.AssignmentModels.AssignmentGroupModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityConfigurations.CourseConfigurations;
using Persistence.EntityConfigurations.RoomConfigurations;

namespace Persistence.EntityConfigurations.AssignmentConfigurations.AssignmentGroupModels;

public class AssignmentGroupConfig : IEntityTypeConfiguration<AssignmentGroup>
{
    public void Configure(EntityTypeBuilder<AssignmentGroup> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).IsRequired();

        builder
            .HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey(x => x.RoomId);

        builder.HasIndex(x => x.Code).IsUnique();

        builder.ToTable("AssignmentGroup");
    }
}