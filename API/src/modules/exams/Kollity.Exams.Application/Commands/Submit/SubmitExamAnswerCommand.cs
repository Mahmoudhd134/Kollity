namespace Kollity.Exams.Application.Commands.Submit;

public record SubmitExamAnswerCommand(Guid QuestionId, Guid OptionId) : ICommand;