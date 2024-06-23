using Kollity.Exams.Domain.ExamModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Exams.Persistence.ExamConfigurations;

public class ExamConfig : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
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