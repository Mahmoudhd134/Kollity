using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Domain.RoomModels;

namespace Kollity.Services.Application.Events.Room;

public record UserJoinRequestSentEvent(UserRoom UserRoom) : IEvent;