using Kollity.Application.Dtos.Assignment;

namespace Kollity.Application.Commands.Assignment.Add;

public record AddAssignmentCommand(Guid RoomId, AddAssignmentDto AddAssignmentDto) : ICommandWithEvents;