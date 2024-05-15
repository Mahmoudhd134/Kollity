using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Room;

public record RoomContentDeletedEvent(Domain.RoomModels.RoomContent RoomContent) : IEvent;