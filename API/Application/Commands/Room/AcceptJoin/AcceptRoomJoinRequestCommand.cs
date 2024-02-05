using Application.Dtos.Room;

namespace Application.Commands.Room.AcceptJoin;

public record AcceptRoomJoinRequestCommand(RoomUserIdsMap Ids) : ICommand;