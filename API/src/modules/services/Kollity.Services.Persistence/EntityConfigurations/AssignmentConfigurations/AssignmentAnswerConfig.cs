using Kollity.Services.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Services.Persistence.EntityConfigurations.AssignmentConfigurations;

public class AssignmentAnswerConfig : IEntityTypeConfiguration<AssignmentAnswer>
{
    public void Configure(EntityTypeBuilder<AssignmentAnswer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.File).HasMaxLength(511).IsRequired();

        builder
            .HasOne(x => x.Assignment)
            .WithMany(x => x.AssignmentsAnswers)
            .HasForeignKey(x => x.AssignmentId);

        builder
            .HasOne(x => x.Student)
            .WithMany(x => x.AssignmentsAnswers)
            .HasForeignKey(x => x.StudentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.AssignmentGroup)
            .WithMany(x => x.AssignmentsAnswers)
            .HasForeignKey(x => x.AssignmentGroupId)
            // .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.ToTable("AssignmentAnswer");
    }
}