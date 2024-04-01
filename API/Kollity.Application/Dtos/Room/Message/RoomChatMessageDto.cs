﻿namespace Kollity.Application.Dtos.Room.Message;

public class RoomChatMessageDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime SentAt { get; set; }
    public bool IsRead { get; set; }
    public RoomChatMessageSender Sender { get; set; }
}