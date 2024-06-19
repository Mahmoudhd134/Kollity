using Kollity.Feedback.Domain.FeedbackModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Feedback.Persistence.Configurations;

public class FeedbackQuestionConfig : IEntityTypeConfiguration<FeedbackQuestion>
{
    public void Configure(EntityTypeBuilder<FeedbackQuestion> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Question).IsRequired().HasMaxLength(1023);

        builder.HasData([
            new()
            {
                Id = Guid.NewGuid(),
                Question = "This is question one for course category",
                Category = FeedbackCategory.Course,
                IsMcq = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Question = "This is question two for course category",
                Category = FeedbackCategory.Course,
                IsMcq = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Question = "This is question three for course category",
                Category = FeedbackCategory.Course,
                IsMcq = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                Question = "This is question one for exam category",
                Category = FeedbackCategory.Exam,
                IsMcq = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Question = "This is question two for exam category",
                Category = FeedbackCategory.Exam,
                IsMcq = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Question = "This is question three for exam category",
                Category = FeedbackCategory.Exam,
                IsMcq = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                Question = "This is question one for doctor category",
                Category = FeedbackCategory.Doctor,
                IsMcq = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Question = "This is question two for doctor category",
                Category = FeedbackCategory.Doctor,
                IsMcq = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Question = "This is question three for doctor category",
                Category = FeedbackCategory.Doctor,
                IsMcq = false
            },
        ]);
        
        builder.ToTable("FeedbackQuestion");
    }
}