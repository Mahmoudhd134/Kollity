using Kollity.Reporting.Domain.ExamModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Exam;

public class ExamAnswerConfig : IEntityTypeConfiguration<ExamAnswer>
{
    public void Configure(EntityTypeBuilder<ExamAnswer> builder)
    {
        builder.HasKey(x => new { x.OptionId, x.StudentId });

        builder
            .HasOne(x => x.Student)
            .WithMany(x => x.ExamAnswers)
            .HasForeignKey(x => x.StudentId);

        builder
            .HasOne(x => x.Option)
            .WithMany(x => x.Answers)
            .HasForeignKey(x => x.OptionId);

        builder.HasIndex(x => x.StudentId);

        builder.ToTable("ExamAnswer");
    }
}