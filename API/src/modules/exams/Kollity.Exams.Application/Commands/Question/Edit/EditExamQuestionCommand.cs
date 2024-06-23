using Kollity.Exams.Application.Dtos.Exam;

namespace Kollity.Exams.Application.Commands.Question.Edit;

public record EditExamQuestionCommand(EditExamQuestionDto Dto) : ICommand;