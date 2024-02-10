using Kollity.Application.Dtos.Room;

namespace Kollity.Application.Commands.Room.AddSupervisor;

public record AddRoomSupervisorCommand(RoomUserIdsMap Ids) : ICommand;