using Kollity.Contracts.Dto;

namespace Kollity.Contracts.Events.Content;

public record RoomContentAddedEvent(
    Guid RoomId,
    string RoomName,
    DateTime AddedAt,
    Guid ContentId,
    string ContentName,
    List<UserEmailDto> Students
) : IEvent;