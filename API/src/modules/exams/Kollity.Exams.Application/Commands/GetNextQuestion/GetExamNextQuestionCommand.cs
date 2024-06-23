using Kollity.Exams.Application.Dtos.Exam;

namespace Kollity.Exams.Application.Commands.GetNextQuestion;

public record GetExamNextQuestionCommand(Guid ExamId) : ICommand<ExamQuestionForAnswerDto>;