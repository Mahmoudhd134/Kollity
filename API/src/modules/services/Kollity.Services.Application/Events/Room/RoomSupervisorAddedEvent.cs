using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Domain.RoomModels;

namespace Kollity.Services.Application.Events.Room;

public record RoomSupervisorAddedEvent(UserRoom UserRoom) : IEvent;