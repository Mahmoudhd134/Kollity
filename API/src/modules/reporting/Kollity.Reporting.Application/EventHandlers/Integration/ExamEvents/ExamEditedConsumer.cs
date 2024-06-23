using Kollity.Exams.Contracts.Exam;
using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.ExamEvents;

public class ExamEditedConsumer(ReportingDbContext context) : IntegrationEventConsumer<ExamEditedIntegrationEvent>
{
    protected override async Task Handle(ExamEditedIntegrationEvent integrationEvent)
    {
        var exam = await context.Exams
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.Id);
        if (exam is null)
            throw new ExamExceptions.ExamNotFound(integrationEvent.Id);


        exam.Name = integrationEvent.Name;
        exam.StartDate = integrationEvent.StartDate;
        exam.EndDate = integrationEvent.EndDate;

        await context.SaveChangesAsync();
    }
}