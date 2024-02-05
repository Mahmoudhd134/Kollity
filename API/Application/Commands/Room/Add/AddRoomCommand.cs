using Application.Dtos.Room;

namespace Application.Commands.Room.Add;

public record AddRoomCommand(AddRoomDto AddRoomDto) : ICommand;