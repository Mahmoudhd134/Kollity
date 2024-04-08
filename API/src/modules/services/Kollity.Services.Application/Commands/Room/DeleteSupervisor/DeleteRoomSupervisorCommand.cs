using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Room;

namespace Kollity.Services.Application.Commands.Room.DeleteSupervisor;

public record DeleteRoomSupervisorCommand(RoomUserIdsMap Ids) : ICommand;