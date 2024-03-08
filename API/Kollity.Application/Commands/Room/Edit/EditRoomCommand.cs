using Kollity.Application.Dtos.Room;

namespace Kollity.Application.Commands.Room.Edit;

public record EditRoomCommand(EditRoomDto EditRoomDto) : ICommand;