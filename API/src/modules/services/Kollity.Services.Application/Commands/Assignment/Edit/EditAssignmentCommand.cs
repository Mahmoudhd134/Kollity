using Kollity.Services.Application.Dtos.Assignment;

namespace Kollity.Services.Application.Commands.Assignment.Edit;

public record EditAssignmentCommand(EditAssignmentDto EditAssignmentDto) : ICommand;