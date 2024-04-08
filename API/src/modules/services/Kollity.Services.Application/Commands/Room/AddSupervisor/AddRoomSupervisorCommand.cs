using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Room;

namespace Kollity.Services.Application.Commands.Room.AddSupervisor;

public record AddRoomSupervisorCommand(RoomUserIdsMap Ids) : ICommand;