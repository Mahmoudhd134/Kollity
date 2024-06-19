using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.EventHandlers.Integration.CourseEvents;

public class CourseDeletedConsumer(FeedbackDbContext context) : IntegrationEventConsumer<CourseDeletedIntegrationEvent>
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