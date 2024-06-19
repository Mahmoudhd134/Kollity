using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Domain;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Course;

namespace Kollity.Feedback.Application.EventHandlers.Integration.CourseEvents;

public class CourseAddedConsumer(FeedbackDbContext context)
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