namespace Kollity.Application.Commands.Exam.Question.Delete;

public record DeleteExamQuestionCommand(Guid QuestionId) : ICommand;