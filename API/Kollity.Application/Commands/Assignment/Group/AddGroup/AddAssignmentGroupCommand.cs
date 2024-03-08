using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Commands.Assignment.Group.AddGroup;

public record AddAssignmentGroupCommand(Guid RoomId, AddAssignmentGroupDto AddAssignmentGroupDto)
<<<<<<< HEAD
    : ICommand<AssignmentGroupDto>;
=======
    : ICommandWithEvents<AssignmentGroupDto>;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
