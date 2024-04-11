using Kollity.Services.Domain.RoomModels;

namespace Kollity.Services.Domain.Identity;

public class BaseUser
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string Email { get; set; }
    public string NormalizedEmail { get; set; }
    public string FullNameInArabic { get; set; }
    public string ProfileImage { get; set; }
    public string Type { get; set; }
    public UserType UserType { get; set; }
    public bool EnabledEmailNotifications { get; set; }
    public List<UserRoom> UsersRooms { get; set; } = [];
}