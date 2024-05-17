using Kollity.Reporting.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Room;

public class RoomContentConfig:IEntityTypeConfiguration<RoomContent>
{
    public void Configure(EntityTypeBuilder<RoomContent> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(511).IsRequired();
        
        builder
            .HasOne(x => x.Uploader)
            .WithMany()
            .HasForeignKey(x => x.UploaderId);

        builder
            .HasOne(x => x.Room)
            .WithMany(x => x.Contents)
            .HasForeignKey(x => x.RoomId);
        
        builder.ToTable("RoomContent");
    }
}