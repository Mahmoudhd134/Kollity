using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Room;

public record RoomEditedEvent(Domain.RoomModels.Room Room) : IEvent;