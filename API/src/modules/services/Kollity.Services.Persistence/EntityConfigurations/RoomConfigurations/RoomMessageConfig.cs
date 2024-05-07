using Kollity.Services.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Kollity.Services.Persistence.EntityConfigurations.RoomConfigurations;

public class RoomMessageConfig : IEntityTypeConfiguration<RoomMessage>
{
    public void Configure(EntityTypeBuilder<RoomMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text).HasMaxLength(4067);

        builder.Property(x => x.File)
            .HasConversion<string>(
                f => JsonConvert.SerializeObject(f),
                s => JsonConvert.DeserializeObject<MessageFile>(s)
            )
            .IsRequired(false)
            .HasMaxLength(1023);

        /*
         * the question length is at most 500 chars
         * the options count is at most 10 and each one is 300 chars at most
         */
        builder.Property(x => x.Poll)
            .HasConversion<string>(
                p => JsonConvert.SerializeObject(p),
                s => JsonConvert.DeserializeObject<MessagePoll>(s)
            )
            .IsRequired(false)
            .HasMaxLength(3650);

        builder
            .HasOne(x => x.Sender)
            .WithMany()
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder
            .HasOne(x => x.Room)
            .WithMany(x => x.RoomMessages)
            .HasForeignKey(x => x.RoomId);

        builder.ToTable("RoomMessage");
    }
}