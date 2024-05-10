using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Room;

public record RoomContentAddedEvent(Domain.RoomModels.RoomContent RoomContent) : IEvent;