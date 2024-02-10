using Kollity.Domain.Identity.User;

namespace Kollity.Domain.RoomModels;

public class RoomMessage
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string File { get; set; }
    public DateTime Date { get; set; }
    public Guid? SenderId { get; set; }
    public BaseUser Sender { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public bool IsRead { get; set; }
}