using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Exam;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.EventHandlers.Integration.ExamEvents;

public class ExamDeletedConsumer(FeedbackDbContext context) : IntegrationEventConsumer<ExamDeletedIntegrationEvent>
{
    protected override async Task Handle(ExamDeletedIntegrationEvent integrationEvent)
    {
        var exam = await context.Exams
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.Id);
        if (exam is null)
            throw new ExamExceptions.ExamNotFound(integrationEvent.Id);

        exam.IsDeleted = true;
        await context.SaveChangesAsync();
    }
}