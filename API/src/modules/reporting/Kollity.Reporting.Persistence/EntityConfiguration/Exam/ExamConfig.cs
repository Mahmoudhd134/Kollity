using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Exam;

public class ExamConfig : IEntityTypeConfiguration<Domain.ExamModels.Exam>
{
    public void Configure(EntityTypeBuilder<Domain.ExamModels.Exam> builder)
    {
        builder.HasKey(x => new { x.ExamId, x.QuestionId, x.OptionId });

        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        builder.Property(x => x.QuestionText).HasMaxLength(1023).IsRequired();
        builder.Property(x => x.Option).HasMaxLength(1023).IsRequired();

        builder
            .HasOne(x => x.Room)
            .WithMany(x => x.Exams)
            .HasForeignKey(x => x.RoomId);
        
        builder
            .HasOne(x => x.Doctor)
            .WithMany()
            .HasForeignKey(x => x.DoctorId);

        builder.HasIndex(x => x.QuestionId);
        builder.HasIndex(x => x.OptionId);

        builder.ToTable("Exam");
    }
}