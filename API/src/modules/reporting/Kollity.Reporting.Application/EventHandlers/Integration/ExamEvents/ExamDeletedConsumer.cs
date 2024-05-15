using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Exam;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.ExamEvents;

public class ExamDeletedConsumer(ReportingDbContext context) : IntegrationEventConsumer<ExamDeletedIntegrationEvent>
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