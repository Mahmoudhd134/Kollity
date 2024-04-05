using Kollity.Application.Abstractions.Events;
using Kollity.Domain.RoomModels;

namespace Kollity.Application.Events.Room.Messages;

public record RoomChatMessageAddedEvent(
    RoomMessage Message,
    Guid SenderId,
    string SenderUserName,
    string SenderImage) : IEvent;