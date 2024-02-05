using Domain.Identity.User;
using Domain.RoomModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations.RoomConfigurations;

public class UserRoomConfig : IEntityTypeConfiguration<UserRoom>
{
    public void Configure(EntityTypeBuilder<UserRoom> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Room)
            .WithMany(x => x.UsersRooms)
            .HasForeignKey(x => x.RoomId);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UsersRooms)
            .HasForeignKey(x => x.UserId);

        builder.HasIndex(x => new { x.UserId, x.RoomId }).IsUnique();
        builder.HasIndex(x => x.IsSupervisor);

        builder.ToTable("UserRoom");
    }
}