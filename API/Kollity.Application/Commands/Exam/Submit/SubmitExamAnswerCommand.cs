namespace Kollity.Application.Commands.Exam.Submit;

public record SubmitExamAnswerCommand(Guid QuestionId, Guid OptionId) : ICommand;