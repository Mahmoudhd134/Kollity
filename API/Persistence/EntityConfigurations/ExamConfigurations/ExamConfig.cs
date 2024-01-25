using Domain.ExamModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityConfigurations.CourseConfigurations;
using Persistence.EntityConfigurations.RoomConfigurations;

namespace Persistence.EntityConfigurations.ExamConfigurations;

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

        builder
            .HasOne(x => x.Course)
            .WithMany()
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("Exam");
    }
}