using Kollity.Application.Dtos.Assignment;

namespace Kollity.Application.Commands.Assignment.Add;

<<<<<<< HEAD
public record AddAssignmentCommand(Guid RoomId, AddAssignmentDto AddAssignmentDto) : ICommand;
=======
public record AddAssignmentCommand(Guid RoomId, AddAssignmentDto AddAssignmentDto) : ICommandWithEvents;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
