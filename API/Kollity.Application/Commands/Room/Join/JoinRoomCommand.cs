﻿namespace Kollity.Application.Commands.Room.Join;

public record JoinRoomCommand(Guid RoomId) : ICommand;