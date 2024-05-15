using Kollity.Services.Application.Dtos.Assignment.Group;

namespace Kollity.Services.Application.Commands.Assignment.Group.AddGroup;

public record AddAssignmentGroupCommand(Guid RoomId, AddAssignmentGroupDto AddAssignmentGroupDto)
    : ICommand<AssignmentGroupDto>;