using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Room;

namespace Kollity.Services.Application.Commands.Room.Edit;

public record EditRoomCommand(EditRoomDto EditRoomDto) : ICommand;