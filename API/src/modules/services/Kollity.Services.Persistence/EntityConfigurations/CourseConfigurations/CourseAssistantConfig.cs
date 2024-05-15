using Kollity.Services.Domain.CourseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Services.Persistence.EntityConfigurations.CourseConfigurations;

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


        builder.HasIndex(x => x.CourseId);
        builder.HasIndex(x => x.AssistantId);
        builder.HasIndex(x => new { x.CourseId, x.AssistantId }).IsUnique();

        builder.ToTable("CourseAssistant");
    }
}