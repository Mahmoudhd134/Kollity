using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.CourseEvents;

public class CourseEditedConsumer(ReportingDbContext context) : IntegrationEventConsumer<CourseEditedIntegrationEvent>
{
    protected override async Task Handle(CourseEditedIntegrationEvent integrationEvent)
    {
        var course = await context.Courses
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.Id);
        if (course is null)
            return;
        course.Id = integrationEvent.Id;
        course.Code = integrationEvent.Code;
        course.Department = integrationEvent.Department;
        course.Hours = integrationEvent.Hours;
        course.Name = integrationEvent.Name;
        await context.SaveChangesAsync();
    }
}