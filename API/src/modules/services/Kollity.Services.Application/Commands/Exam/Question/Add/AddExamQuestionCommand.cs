using Kollity.Services.Application.Dtos.Exam;

namespace Kollity.Services.Application.Commands.Exam.Question.Add;

public record AddExamQuestionCommand(Guid ExamId, AddExamQuestionDto Dto) : ICommand<ExamQuestionDto>;