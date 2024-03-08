using Kollity.Application.Dtos.Room;

namespace Kollity.Application.Commands.Room.DenyJoin;

public record DenyRoomJoinRequestCommand(RoomUserIdsMap Ids) : ICommand;