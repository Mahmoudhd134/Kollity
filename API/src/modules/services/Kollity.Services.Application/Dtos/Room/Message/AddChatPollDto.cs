namespace Kollity.Services.Application.Dtos.Room.Message;

public class AddChatPollDto
{
    public string Question { get; set; }
    public List<string> Options { get; set; } = [];
}