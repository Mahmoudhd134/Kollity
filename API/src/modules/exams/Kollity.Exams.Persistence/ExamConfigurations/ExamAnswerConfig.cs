﻿using Kollity.Exams.Domain.ExamModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Exams.Persistence.ExamConfigurations;

public class ExamAnswerConfig : IEntityTypeConfiguration<ExamAnswer>
{
    public void Configure(EntityTypeBuilder<ExamAnswer> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.Exam)
            .WithMany(x => x.Answers)
            .HasForeignKey(x => x.ExamId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.ExamQuestion)
            .WithMany(x => x.ExamAnswers)
            .HasForeignKey(x => x.ExamQuestionId);

        builder
            .HasOne(x => x.ExamQuestionOption)
            .WithMany(x => x.ExamAnswers)
            .HasForeignKey(x => x.ExamQuestionOptionId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        builder.HasIndex(x => new { x.StudentId, x.ExamQuestionId }).IsUnique();

        builder.ToTable("ExamAnswer");
    }
}