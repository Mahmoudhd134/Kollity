namespace Kollity.Services.Application.Dtos.Room.Message;

public class ChatPollDto
{
    public bool IsMultiAnswer { get; set; }
    public byte MaxOptionsCountForSubmission { get; set; }
    public string Question { get; set; }
    public List<ChatPollOptionDto> Options { get; set; }
}