using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Room;

namespace Kollity.Services.Application.Commands.Room.Add;

public record AddRoomCommand(AddRoomDto AddRoomDto) : ICommand;