using Kollity.Application.Dtos.Room;

namespace Kollity.Application.Commands.Room.AcceptJoin;

public record AcceptRoomJoinRequestCommand(RoomUserIdsMap Ids) : ICommand;