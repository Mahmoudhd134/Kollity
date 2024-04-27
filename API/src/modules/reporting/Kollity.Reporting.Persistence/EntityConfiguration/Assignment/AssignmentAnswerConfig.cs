using Kollity.Reporting.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Assignment;

public class AssignmentAnswerConfig : IEntityTypeConfiguration<AssignmentAnswer>
{
    public void Configure(EntityTypeBuilder<AssignmentAnswer> builder)
    {
        builder.HasKey(x => new { x.StudentId, x.AssignmentId });

        builder
            .HasOne(x => x.Student)
            .WithMany(x => x.AssignmentAnswers)
            .HasForeignKey(x => x.StudentId);

        builder
            .HasOne(x => x.Assignment)
            .WithMany(x => x.AssignmentsAnswers)
            .HasForeignKey(x => x.AssignmentId);

        builder
            .HasOne(x => x.Group)
            .WithMany()
            .HasForeignKey(x => new { x.GroupId, x.StudentId });

        builder
            .HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey(x => x.RoomId)
            .IsRequired(false);

        builder.ToTable("AssignmentAnswer");
    }
}