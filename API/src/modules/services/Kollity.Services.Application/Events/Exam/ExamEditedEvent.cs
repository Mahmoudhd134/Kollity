using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Exam;

public record ExamEditedEvent(Domain.ExamModels.Exam Exam) : IEvent;