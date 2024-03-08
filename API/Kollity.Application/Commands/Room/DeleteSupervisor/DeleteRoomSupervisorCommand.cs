using Kollity.Application.Dtos.Room;

namespace Kollity.Application.Commands.Room.DeleteSupervisor;

public record DeleteRoomSupervisorCommand(RoomUserIdsMap Ids) : ICommand;