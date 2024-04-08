using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Exam.Submit;

public record SubmitExamAnswerCommand(Guid QuestionId, Guid OptionId) : ICommand;