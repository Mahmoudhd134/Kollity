using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kollity.Feedback.Application.EventHandlers.Integration.CourseEvents;

public class StudentDeAssignedFromCourseConsumer(
    FeedbackDbContext context,
    ILogger<StudentDeAssignedFromCourseConsumer> logger)
    : IntegrationEventConsumer<StudentDeAssignedFromCourseIntegrationEvent>
{
    protected override async Task Handle(StudentDeAssignedFromCourseIntegrationEvent integrationEvent)
    {
        Guid sId = integrationEvent.StudentId,
            cId = integrationEvent.CourseId;

        var courseStudent = await context.CourseStudents
            .Where(x =>
                x.CourseId == cId &&
                x.StudentId == sId
            )
            .FirstOrDefaultAsync();

        if (courseStudent == null)
        {
            logger.LogError(
                "Trying to de assign the student {StudentId} from course {CourseId}, but the student is not assigned to the course",
                sId, cId);
            throw new CourseExceptions.StudentIsNotAssigned(sId, cId);
        }

        context.Remove(courseStudent);
        await context.SaveChangesAsync();
    }
}