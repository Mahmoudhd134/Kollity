using Application.Dtos.Room;

namespace Application.Commands.Room.DenyJoin;

public record DenyRoomJoinRequestCommand(RoomUserIdsMap Ids) : ICommand;