using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Commands.Assignment.Group.DeleteStudent;

public record DeleteStudentFromAssignmentGroupCommand(AssignmentGroupInvitationMapDto InvitationDto) : ICommand;