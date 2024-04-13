namespace Kollity.Services.Application.Dtos.Room.Message;

public class ChatPollDto
{
    public string Question { get; set; }
    public List<ChatPollOptionDto> Options { get; set; }
}