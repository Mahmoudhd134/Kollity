namespace Kollity.Services.Application.Dtos.Room.Message;

public class AddChatPollDto
{
    public string Question { get; set; }
    public bool IsMultiAnswer { get; set; }
    public byte MaxOptionsCountForSubmission { get; set; }
    public List<string> Options { get; set; } = [];
}