using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Exam;

public record ExamAddedEvent(Domain.ExamModels.Exam Exam) : IEvent;