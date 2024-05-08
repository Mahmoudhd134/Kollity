using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Exam;

public class ExamConfig : IEntityTypeConfiguration<Domain.ExamModels.Exam>
{
    public void Configure(EntityTypeBuilder<Domain.ExamModels.Exam> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();

        builder
            .HasOne(x => x.Room)
            .WithMany(x => x.Exams)
            .HasForeignKey(x => x.RoomId);

        builder.ToTable("Exam");
    }
}