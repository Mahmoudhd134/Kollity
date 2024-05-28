using Kollity.Services.Domain.FeedbackModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Services.Persistence.EntityConfigurations.FeedbackConfigurations;

public class FeedbackQuestionConfig : IEntityTypeConfiguration<FeedbackQuestion>
{
    public void Configure(EntityTypeBuilder<FeedbackQuestion> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Question).IsRequired().HasMaxLength(1023);
        builder.ToTable("FeedbackQuestion");
    }
}

public class FeedbackAnswerConfig : IEntityTypeConfiguration<FeedbackAnswer>
{
    public void Configure(EntityTypeBuilder<FeedbackAnswer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.StringAnswer).IsRequired(false).HasMaxLength(4095);

        builder
            .HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.StudentId);

        builder
            .HasOne(x => x.Question)
            .WithMany()
            .HasForeignKey(x => x.QuestionId);

        builder
            .HasOne(x => x.Exam)
            .WithMany()
            .HasForeignKey(x => x.ExamId)
            .IsRequired(false);

        builder
            .HasOne(x => x.Course)
            .WithMany()
            .HasForeignKey(x => x.CourseId)
            .IsRequired(false);

        builder
            .HasOne(x => x.Doctor)
            .WithMany()
            .HasForeignKey(x => x.DoctorId)
            .IsRequired(false);

        builder.ToTable("FeedbackAnswer");
    }
}