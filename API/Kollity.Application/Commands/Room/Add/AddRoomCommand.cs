using Kollity.Application.Dtos.Room;

namespace Kollity.Application.Commands.Room.Add;

public record AddRoomCommand(AddRoomDto AddRoomDto) : ICommand;