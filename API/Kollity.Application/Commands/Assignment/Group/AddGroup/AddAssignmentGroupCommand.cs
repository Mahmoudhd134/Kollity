using Kollity.Application.Dtos.Assignment;
using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Commands.Assignment.Group.AddGroup;

public record AddAssignmentGroupCommand(Guid RoomId, AddAssignmentGroupDto AddAssignmentGroupDto)
    : ICommand<AssignmentGroupDto>;