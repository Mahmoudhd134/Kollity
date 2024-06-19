using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Exam;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.EventHandlers.Integration.ExamEvents;

public class ExamEditedConsumer(FeedbackDbContext context) : IntegrationEventConsumer<ExamEditedIntegrationEvent>
{
    protected override async Task Handle(ExamEditedIntegrationEvent integrationEvent)
    {
        var exam = await context.Exams
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.Id);
        if (exam is null)
            throw new ExamExceptions.ExamNotFound(integrationEvent.Id);


        exam.Name = integrationEvent.Name;

        await context.SaveChangesAsync();
    }
}