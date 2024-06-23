using Kollity.Exams.Application.Dtos.Exam;

namespace Kollity.Exams.Application.Commands.Question.Add;

public record AddExamQuestionCommand(Guid ExamId, AddExamQuestionDto Dto) : ICommand<ExamQuestionDto>;