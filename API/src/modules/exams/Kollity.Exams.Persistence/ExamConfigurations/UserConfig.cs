using Kollity.Exams.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Exams.Persistence.ExamConfigurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserName).IsRequired().HasMaxLength(511);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(511);
        builder.Property(x => x.Code).HasMaxLength(15);

        builder.ToTable("User");
    }
}