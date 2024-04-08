namespace Kollity.Services.Domain.RoomModels;

public class MessagePoll
{
    public string Question { get; set; }
    public List<string> Options { get; set; }
}