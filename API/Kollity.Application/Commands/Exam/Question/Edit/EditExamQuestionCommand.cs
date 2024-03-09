using Kollity.Application.Dtos.Exam;

namespace Kollity.Application.Commands.Exam.Question.Edit;

public record EditExamQuestionCommand(EditExamQuestionDto Dto) : ICommand;