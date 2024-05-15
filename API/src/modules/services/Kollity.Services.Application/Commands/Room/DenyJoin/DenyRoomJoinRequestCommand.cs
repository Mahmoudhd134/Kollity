using Kollity.Services.Application.Dtos.Room;

namespace Kollity.Services.Application.Commands.Room.DenyJoin;

public record DenyRoomJoinRequestCommand(RoomUserIdsMap Ids) : ICommand;