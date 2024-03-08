using Kollity.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Persistence.EntityConfigurations.AssignmentConfigurations;

public class AssignmentAnswerDegreeConfig : IEntityTypeConfiguration<AssignmentAnswerDegree>
{
    public void Configure(EntityTypeBuilder<AssignmentAnswerDegree> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.Group)
            .WithMany()
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .HasOne(x => x.Assignment)
            .WithMany()
            .HasForeignKey(x => x.AssignmentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.Answer)
            .WithMany(x => x.GroupDegrees)
            .HasForeignKey(x => x.AnswerId);

        builder.ToTable("AssignmentAnswerDegree");
    }
}