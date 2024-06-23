using Kollity.Exams.Application.Dtos.Exam;

namespace Kollity.Exams.Application.Commands.Question.Option.Add;

public record AddExamQuestionOptionCommand(Guid QuestionId, AddExamQuestionOptionDto Dto) : ICommand<Guid>;