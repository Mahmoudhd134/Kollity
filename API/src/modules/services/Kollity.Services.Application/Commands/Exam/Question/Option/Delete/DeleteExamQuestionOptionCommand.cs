using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Exam.Question.Option.Delete;

public record DeleteExamQuestionOptionCommand(Guid OptionId) : ICommand;