using Kollity.Application.Dtos.Exam;

namespace Kollity.Application.Commands.Exam.Question.Option.Add;

public record AddExamQuestionOptionCommand(Guid QuestionId, AddExamQuestionOptionDto Dto) : ICommand<Guid>;