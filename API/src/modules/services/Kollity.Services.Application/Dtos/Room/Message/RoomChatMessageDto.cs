using Kollity.Services.Domain.RoomModels;

namespace Kollity.Services.Application.Dtos.Room.Message;

public class RoomChatMessageDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime SentAt { get; set; }
    public bool IsRead { get; set; }
    public string FileName { get; set; }
    public ChatPollDto Poll { get; set; }
    public RoomMessageType Type { get; set; }
    public RoomChatMessageSenderDto SenderDto { get; set; }
}