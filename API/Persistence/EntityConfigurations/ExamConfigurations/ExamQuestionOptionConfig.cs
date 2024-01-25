using Domain.ExamModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations.ExamConfigurations;

public class ExamQuestionOptionConfig : IEntityTypeConfiguration<ExamQuestionOption>
{
    public void Configure(EntityTypeBuilder<ExamQuestionOption> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Option).HasMaxLength(1023).IsRequired();

        builder
            .HasOne(x => x.ExamQuestion)
            .WithMany(x => x.ExamQuestionOptions)
            .HasForeignKey(x => x.ExamQuestionId);

        builder
            .HasOne(x => x.Exam)
            .WithMany()
            .HasForeignKey(x => x.ExamId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("ExamQuestionOption");
    }
}