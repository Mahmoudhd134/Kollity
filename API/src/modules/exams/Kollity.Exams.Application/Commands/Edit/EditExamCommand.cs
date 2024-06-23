using Kollity.Exams.Application.Dtos.Exam;

namespace Kollity.Exams.Application.Commands.Edit;

public record EditExamCommand(EditExamDto Dto) : ICommand;