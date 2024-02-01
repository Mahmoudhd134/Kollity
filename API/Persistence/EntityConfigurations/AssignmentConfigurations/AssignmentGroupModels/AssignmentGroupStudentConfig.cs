using Domain.AssignmentModels.AssignmentGroupModels;
using Domain.StudentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityConfigurations.CourseConfigurations;
using Persistence.EntityConfigurations.RoomConfigurations;

namespace Persistence.EntityConfigurations.AssignmentConfigurations.AssignmentGroupModels;

public class AssignmentGroupStudentConfig : IEntityTypeConfiguration<AssignmentGroupStudent>
{
    public void Configure(EntityTypeBuilder<AssignmentGroupStudent> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.AssignmentGroup)
            .WithMany(x => x.AssignmentGroupsStudents)
            .HasForeignKey(x => x.AssignmentGroupId);

        builder
            .HasOne(x => x.Student)
            .WithMany(x => x.AssignmentGroupsStudents)
            .HasForeignKey(x => x.StudentId);

        // builder
        //     .HasOne(x => x.Room)
        //     .WithMany()
        //     .HasForeignKey(x => x.RoomId);

        builder.ToTable("AssignmentGroupStudent");
    }
}