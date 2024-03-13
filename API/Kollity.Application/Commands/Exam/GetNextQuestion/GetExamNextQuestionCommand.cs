using Kollity.Application.Dtos.Exam;

namespace Kollity.Application.Commands.Exam.GetNextQuestion;

public record GetExamNextQuestionCommand(Guid ExamId) : ICommand<ExamQuestionForAnswerDto>;