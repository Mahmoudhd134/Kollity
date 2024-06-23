namespace Kollity.Exams.Application.Commands.Question.Option.Delete;

public record DeleteExamQuestionOptionCommand(Guid OptionId) : ICommand;