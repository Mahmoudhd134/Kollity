using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.CourseEvents;

public class CourseDeletedConsumer(ReportingDbContext context) : IntegrationEventConsumer<CourseDeletedIntegrationEvent>
{
    protected override async Task Handle(CourseDeletedIntegrationEvent integrationEvent)
    {
        var result = await context.Courses
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsDeleted, true));
        if (result == 0)
            throw new CourseExceptions.CourseNotFound(integrationEvent.Id);
    }
}