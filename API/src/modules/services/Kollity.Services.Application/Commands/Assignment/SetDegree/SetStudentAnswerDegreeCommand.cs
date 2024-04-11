using Kollity.Services.Application.Dtos.Assignment;

namespace Kollity.Services.Application.Commands.Assignment.SetDegree;

public record SetStudentAnswerDegreeCommand(SetAnswerDegreeDto Dto) : ICommand;