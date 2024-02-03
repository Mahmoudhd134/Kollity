using Domain.StudentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityConfigurations.CourseConfigurations;

namespace Persistence.EntityConfigurations.StudentConfigurations;

public class StudentCourseConfig : IEntityTypeConfiguration<StudentCourse>
{
    public void Configure(EntityTypeBuilder<StudentCourse> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Course)
            .WithMany(x => x.StudentsCourses)
            .HasForeignKey(x => x.CourseId);

        builder
            .HasOne(x => x.Student)
            .WithMany(x => x.StudentsCourses)
            .HasForeignKey(x => x.StudentId);

        builder
            .HasIndex(x => new { x.StudentId, x.CourseId })
            .IsUnique();
        builder.ToTable("StudentCourse");
    }
}