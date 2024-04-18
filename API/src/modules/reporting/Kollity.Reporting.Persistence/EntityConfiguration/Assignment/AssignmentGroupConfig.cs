using Kollity.Reporting.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Assignment;

public class AssignmentGroupConfig : IEntityTypeConfiguration<AssignmentGroup>
{
    public void Configure(EntityTypeBuilder<AssignmentGroup> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.StudentId);
        
        builder
            .HasOne(x => x.Room)
            .WithMany(x => x.AssignmentGroups)
            .HasForeignKey(x => x.RoomId);
        
        builder.HasIndex(x => new { x.RoomId, x.StudentId }).IsUnique();

        builder.ToTable("AssignmentGroup");
    }
}