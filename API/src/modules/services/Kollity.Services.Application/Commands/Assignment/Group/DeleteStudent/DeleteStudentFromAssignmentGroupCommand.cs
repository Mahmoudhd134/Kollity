using Kollity.Services.Application.Dtos.Assignment.Group;

namespace Kollity.Services.Application.Commands.Assignment.Group.DeleteStudent;

public record DeleteStudentFromAssignmentGroupCommand(AssignmentGroupInvitationMapDto InvitationDto) : ICommand;