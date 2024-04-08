using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Exam.Question.Option.MakeRightOption;

public record MakeExamQuestionOptionIsTheRightOptionCommand(Guid OptionId) : ICommand;