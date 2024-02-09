using Domain.Identity.Role;
using Domain.RoomModels;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity.User;

public class BaseUser : IdentityUser<Guid>
{
    public string ProfileImage { get; set; }
    public string Type { get; set; }
    public List<UserRefreshToken.UserRefreshToken> UserRefreshTokens { get; set; } = [];
    public List<UserRoom> UsersRooms { get; set; } = [];

    public List<IdentityUserRole<Guid>> Roles { get; set; }
    // public List<RoomSupervisor> RoomsSupervisors { get; set; } = [];
}