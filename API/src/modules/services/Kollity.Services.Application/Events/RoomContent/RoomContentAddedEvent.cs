using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.RoomContent;

public record RoomContentAddedEvent(Domain.RoomModels.RoomContent RoomContent) : IEvent;