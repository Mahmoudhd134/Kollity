using Application.Dtos.Room;

namespace Application.Commands.Room.AddSupervisor;

public record AddRoomSupervisorCommand(RoomUserIdsMap Ids) : ICommand;