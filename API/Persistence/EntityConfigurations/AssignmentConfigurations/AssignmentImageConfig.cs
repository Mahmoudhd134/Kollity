using Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations.AssignmentConfigurations;

public class AssignmentImageConfig : IEntityTypeConfiguration<AssignmentImage>
{
    public void Configure(EntityTypeBuilder<AssignmentImage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Image).HasMaxLength(511).IsRequired();

        builder
            .HasOne(x => x.Assignment)
            .WithMany(x => x.AssignmentImages)
            .HasForeignKey(x => x.AssignmentId);

        builder.ToTable("AssignmentImage");
    }
}