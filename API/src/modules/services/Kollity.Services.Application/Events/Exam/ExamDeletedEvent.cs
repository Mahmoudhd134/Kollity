using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Exam;

public record ExamDeletedEvent(Domain.ExamModels.Exam Exam) : IEvent;