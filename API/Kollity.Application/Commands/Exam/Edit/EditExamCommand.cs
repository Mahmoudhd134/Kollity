using Kollity.Application.Dtos.Exam;

namespace Kollity.Application.Commands.Exam.Edit;

public record EditExamCommand(EditExamDto Dto) : ICommand;