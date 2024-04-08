using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Exam;

namespace Kollity.Services.Application.Commands.Exam.Question.Option.Add;

public record AddExamQuestionOptionCommand(Guid QuestionId, AddExamQuestionOptionDto Dto) : ICommand<Guid>;