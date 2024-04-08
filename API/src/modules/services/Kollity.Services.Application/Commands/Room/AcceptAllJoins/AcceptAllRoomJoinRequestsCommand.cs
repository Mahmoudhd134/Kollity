using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Room.AcceptAllJoins;

public record AcceptAllRoomJoinRequestsCommand(Guid RoomId) : ICommand;