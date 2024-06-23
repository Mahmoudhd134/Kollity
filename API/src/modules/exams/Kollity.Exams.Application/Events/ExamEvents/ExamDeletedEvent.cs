using Kollity.Exams.Application.Abstractions.Events;

namespace Kollity.Exams.Application.Events.ExamEvents;

public record ExamDeletedEvent(Domain.ExamModels.Exam Exam) : IEvent;