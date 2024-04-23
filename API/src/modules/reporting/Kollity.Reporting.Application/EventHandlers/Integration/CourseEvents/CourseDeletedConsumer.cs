using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.CourseEvents;

public class CourseDeletedConsumer(ReportingDbContext context) : IntegrationEventConsumer<CourseDeletedIntegrationEvent>
{
    protected override Task Handle(CourseDeletedIntegrationEvent integrationEvent)
    {
        return context.Courses
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsDeleted, true));
    }
}