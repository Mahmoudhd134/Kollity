using Kollity.Services.Application.Dtos.Assignment;

namespace Kollity.Services.Application.Commands.Assignment.Answer;

public record AddAssignmentAnswerCommand(Guid AssignmentId, AddAssignmentAnswerDto AddAssignmentAnswerDto) : ICommand;