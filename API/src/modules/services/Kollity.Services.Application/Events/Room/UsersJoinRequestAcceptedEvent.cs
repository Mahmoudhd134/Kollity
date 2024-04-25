using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Room;

public record UsersJoinRequestAcceptedEvent(List<Guid> UsersId, Guid RoomId) : IEvent;