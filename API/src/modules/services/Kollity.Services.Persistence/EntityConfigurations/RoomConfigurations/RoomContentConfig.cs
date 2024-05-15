using Kollity.Services.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Services.Persistence.EntityConfigurations.RoomConfigurations;

public class RoomContentConfig : IEntityTypeConfiguration<RoomContent>
{
    public void Configure(EntityTypeBuilder<RoomContent> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FilePath).HasMaxLength(511).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(511).IsRequired();

        builder
            .HasOne(x => x.Room)
            .WithMany(x => x.RoomContents)
            .HasForeignKey(x => x.RoomId);

        builder
            .HasOne(x => x.Uploader)
            .WithMany()
            .HasForeignKey(x => x.UploaderId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable("RoomContent");
    }
}