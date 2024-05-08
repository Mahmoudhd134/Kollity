using Kollity.Reporting.Domain.ExamModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Exam;

public class ExamQuestionConfig : IEntityTypeConfiguration<ExamQuestion>
{
    public void Configure(EntityTypeBuilder<ExamQuestion> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Question).HasMaxLength(1023).IsRequired();

        builder
            .HasOne(x => x.Exam)
            .WithMany(x => x.ExamQuestions)
            .HasForeignKey(x => x.ExamId);

        builder.ToTable("ExamQuestion");
    }
}