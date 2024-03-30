using Microsoft.AspNetCore.Http;

namespace Kollity.Application.Dtos.Room.Message;

public class AddRoomMessageDto
{
    public string Text { get; set; }
    public IFormFile File { get; set; }
}