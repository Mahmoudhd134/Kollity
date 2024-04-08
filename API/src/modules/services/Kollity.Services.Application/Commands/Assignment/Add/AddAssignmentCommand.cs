using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment;

namespace Kollity.Services.Application.Commands.Assignment.Add;

public record AddAssignmentCommand(Guid RoomId, AddAssignmentDto AddAssignmentDto) : ICommand;