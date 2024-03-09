using Kollity.Application.Dtos.Exam;

namespace Kollity.Application.Commands.Exam.Question.Add;

public record AddExamQuestionCommand(Guid ExamId, AddExamQuestionDto Dto) : ICommand<Guid>;