using Kollity.Application.Abstractions.Events;

namespace Kollity.Application.IntegrationEvents.RoomContent;

public record RoomContentAddedEvent(Domain.RoomModels.RoomContent RoomContent) : IEvent;