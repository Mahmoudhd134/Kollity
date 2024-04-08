using Kollity.Services.Domain.RoomModels;
using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Room.Messages;

public record RoomChatMessageAddedEvent(
    RoomMessage Message,
    Guid SenderId,
    string SenderUserName,
    string SenderImage) : IEvent;