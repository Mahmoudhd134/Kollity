using Application.Dtos.Room;

namespace Application.Commands.Room.DeleteSupervisor;

public record DeleteRoomSupervisorCommand(RoomUserIdsMap Ids) : ICommand;