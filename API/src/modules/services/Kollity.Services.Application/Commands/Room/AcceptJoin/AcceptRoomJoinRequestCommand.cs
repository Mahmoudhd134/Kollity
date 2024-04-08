using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Room;

namespace Kollity.Services.Application.Commands.Room.AcceptJoin;

public record AcceptRoomJoinRequestCommand(RoomUserIdsMap Ids) : ICommand;