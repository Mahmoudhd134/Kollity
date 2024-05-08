using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Domain.ExamModels;

namespace Kollity.Services.Application.Events.Exam;

public record ExamQuestionOptionMarkedAsRightOptionEvent(ExamQuestionOption ExamQuestionOption) : IEvent;