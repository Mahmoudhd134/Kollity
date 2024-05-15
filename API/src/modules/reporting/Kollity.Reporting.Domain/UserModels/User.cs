using Kollity.Reporting.Domain.RoomModels;

namespace Kollity.Reporting.Domain.UserModels;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullNameInArabic { get; set; }
    public string ProfileImage { get; set; }
    public bool IsDeleted { get; set; }
    public string Type { get; set; }
    public List<RoomUser> RoomUsers { get; set; }
}