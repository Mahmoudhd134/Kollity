namespace Kollity.Exams.Application.Commands.Question.Delete;

public record DeleteExamQuestionCommand(Guid QuestionId) : ICommand;