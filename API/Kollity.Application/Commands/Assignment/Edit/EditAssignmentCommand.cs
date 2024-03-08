using Kollity.Application.Dtos.Assignment;

namespace Kollity.Application.Commands.Assignment.Edit;

public record EditAssignmentCommand(EditAssignmentDto EditAssignmentDto) : ICommand;