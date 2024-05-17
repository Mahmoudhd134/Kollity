using Kollity.Services.Domain.Identity;

namespace Kollity.Services.Domain.RoomModels;

public class RoomMessage
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }
    public Guid? SenderId { get; set; }
    public BaseUser Sender { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public bool IsRead { get; set; }
    public RoomMessageType Type { get; set; }
    public bool IsPinned { get; set; }
    public MessageFile File { get; set; }
    public MessagePoll Poll { get; set; }
    public List<MessagePollAnswer> PollAnswers { get; set; } = [];
}