using Kollity.Application.Abstractions.Events;

namespace Kollity.Application.Events.Exam;

public record ExamAddedEvent(Domain.ExamModels.Exam Exam) : IEvent;