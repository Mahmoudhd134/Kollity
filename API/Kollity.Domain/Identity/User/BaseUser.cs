using Kollity.Domain.RoomModels;
using Microsoft.AspNetCore.Identity;

namespace Kollity.Domain.Identity.User;

public class BaseUser : IdentityUser<Guid>
{
    public string FullNameInArabic { get; set; }
    public string ProfileImage { get; set; }
    public string Type { get; set; }
<<<<<<< HEAD
    public List<UserRefreshToken.UserRefreshToken> UserRefreshTokens { get; set; } = [];
    public List<UserRoom> UsersRooms { get; set; } = [];

    public List<IdentityUserRole<Guid>> Roles { get; set; }
    // public List<RoomSupervisor> RoomsSupervisors { get; set; } = [];
=======
    public bool EnabledEmailNotifications { get; set; }
    public List<UserRefreshToken.UserRefreshToken> UserRefreshTokens { get; set; } = [];
    public List<UserRoom> UsersRooms { get; set; } = [];
    public List<IdentityUserRole<Guid>> Roles { get; set; }
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
}