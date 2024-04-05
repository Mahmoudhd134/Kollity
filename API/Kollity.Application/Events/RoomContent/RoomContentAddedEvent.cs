using Kollity.Application.Abstractions.Events;

namespace Kollity.Application.Events.RoomContent;

public record RoomContentAddedEvent(Domain.RoomModels.RoomContent RoomContent) : IEvent;