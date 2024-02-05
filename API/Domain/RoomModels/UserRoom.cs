using Domain.Identity.User;

namespace Domain.RoomModels;

public class UserRoom
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public Guid UserId { get; set; }
    public BaseUser User { get; set; }

    public bool IsSupervisor { get; set; }
    public bool JoinRequestAccepted { get; set; }
    public DateTime LastOnlineDate { get; set; }
}