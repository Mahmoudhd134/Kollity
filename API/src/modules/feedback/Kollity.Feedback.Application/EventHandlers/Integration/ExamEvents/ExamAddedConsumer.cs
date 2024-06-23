using Kollity.Exams.Contracts.Exam;
using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Domain;
using Kollity.Feedback.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.EventHandlers.Integration.ExamEvents;

public class ExamAddedConsumer(FeedbackDbContext context) : IntegrationEventConsumer<ExamAddedIntegrationEvent>
{
    protected override async Task Handle(ExamAddedIntegrationEvent integrationEvent)
    {
        var roomExists = await context.Rooms
            .AnyAsync(x => x.Id == integrationEvent.RoomId);
        if (roomExists == false)
            throw new RoomExceptions.RoomNotFound(integrationEvent.RoomId);
        var exam = new Exam
        {
            Id = integrationEvent.Id,
            Name = integrationEvent.Name,
            RoomId = integrationEvent.RoomId
        };
        context.Exams.Add(exam);
        await context.SaveChangesAsync();
    }
}