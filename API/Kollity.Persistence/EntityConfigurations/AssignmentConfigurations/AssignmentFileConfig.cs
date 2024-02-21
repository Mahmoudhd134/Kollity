using Kollity.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Persistence.EntityConfigurations.AssignmentConfigurations;

public class AssignmentFileConfig : IEntityTypeConfiguration<AssignmentFile>
{
    public void Configure(EntityTypeBuilder<AssignmentFile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FilePath).HasMaxLength(511).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(511).IsRequired();

        builder
            .HasOne(x => x.Assignment)
            .WithMany(x => x.AssignmentFiles)
            .HasForeignKey(x => x.AssignmentId);

        builder.ToTable("AssignmentFile");
    }
}