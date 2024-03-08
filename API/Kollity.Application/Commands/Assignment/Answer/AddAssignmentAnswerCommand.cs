using Kollity.Application.Dtos.Assignment;

namespace Kollity.Application.Commands.Assignment.Answer;

public record AddAssignmentAnswerCommand(Guid AssignmentId, AddAssignmentAnswerDto AddAssignmentAnswerDto) : ICommand;