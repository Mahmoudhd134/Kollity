using Application.Dtos.Room;

namespace Application.Commands.Room.Edit;

public record EditRoomCommand(EditRoomDto EditRoomDto) : ICommand;