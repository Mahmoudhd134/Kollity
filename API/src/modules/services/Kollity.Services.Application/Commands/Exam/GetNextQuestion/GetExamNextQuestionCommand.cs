using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Exam;

namespace Kollity.Services.Application.Commands.Exam.GetNextQuestion;

public record GetExamNextQuestionCommand(Guid ExamId) : ICommand<ExamQuestionForAnswerDto>;