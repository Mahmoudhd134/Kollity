using Kollity.Application.Abstractions.Events;

namespace Kollity.Application.IntegrationEvents.Exam;

public record ExamAddedEvent(Domain.ExamModels.Exam Exam) : IEvent;