namespace Kollity.Application.Commands.Room.AcceptAllJoins;

public record AcceptAllRoomJoinRequestsCommand(Guid RoomId) : ICommand;