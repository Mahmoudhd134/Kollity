namespace Kollity.Services.Domain.RoomModels;

public class MessagePoll
{
    public string Question { get; set; }
    public bool IsMultiAnswer { get; set; }
    public byte MaxOptionsCountForSubmission { get; set; }
    public List<string> Options { get; set; }
}