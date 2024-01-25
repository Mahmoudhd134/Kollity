using Domain.CourseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityConfigurations.AssistantConfigurations;

namespace Persistence.EntityConfigurations.CourseConfigurations;

public class CourseAssistantConfig : IEntityTypeConfiguration<CourseAssistant>
{
    public void Configure(EntityTypeBuilder<CourseAssistant> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Course)
            .WithMany(x => x.CoursesAssistants)
            .HasForeignKey(x => x.CourseId);

        builder
            .HasOne(x => x.Assistant)
            .WithMany(x => x.CoursesAssistants)
            .HasForeignKey(x => x.AssistantId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("CourseAssistant");
    }
}