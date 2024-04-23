using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Domain.CourseModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.Extensions.Logging;

namespace Kollity.Reporting.Application.EventHandlers.Integration.CourseEvents;

public class CourseAddedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<CourseAddedIntegrationEvent>
{
    protected override Task Handle(CourseAddedIntegrationEvent integrationEvent)
    {
        var course = new Course()
        {
            Id = integrationEvent.Id,
            Code = integrationEvent.Code,
            Department = integrationEvent.Department,
            Hours = integrationEvent.Hours,
            Name = integrationEvent.Name,
            IsDeleted = false
        };
        context.Courses.Add(course);
        return context.SaveChangesAsync();
    }
}