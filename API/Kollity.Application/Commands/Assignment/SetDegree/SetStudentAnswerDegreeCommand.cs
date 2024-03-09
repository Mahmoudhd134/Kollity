using Kollity.Application.Dtos.Assignment;

namespace Kollity.Application.Commands.Assignment.SetDegree;

public record SetStudentAnswerDegreeCommand(SetAnswerDegreeDto Dto) : ICommandWithEvents;