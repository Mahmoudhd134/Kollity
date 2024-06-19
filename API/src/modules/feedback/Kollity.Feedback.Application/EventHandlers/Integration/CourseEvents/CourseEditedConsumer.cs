using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.EventHandlers.Integration.CourseEvents;

public class CourseEditedConsumer(FeedbackDbContext context) : IntegrationEventConsumer<CourseEditedIntegrationEvent>
{
    protected override async Task Handle(CourseEditedIntegrationEvent integrationEvent)
    {
        var course = await context.Courses
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.Id);
        if (course is null)
            throw new CourseExceptions.CourseNotFound(integrationEvent.Id);
        course.Id = integrationEvent.Id;
        course.Code = integrationEvent.Code;
        course.Department = integrationEvent.Department;
        course.Hours = integrationEvent.Hours;
        course.Name = integrationEvent.Name;
        await context.SaveChangesAsync();
    }
}