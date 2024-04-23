using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kollity.Reporting.Application.EventHandlers.Integration.CourseEvents;

public class StudentDeAssignedFromCourseConsumer(
    ReportingDbContext context,
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
                x.StudentId == sId &&
                x.IsCurrentlyAssigned)
            .FirstOrDefaultAsync();

        if (courseStudent == null)
        {
            logger.LogError(
                "Trying to de assign the student {StudentId} from course {CourseId}, but the student is not assigned to the course",
                sId, cId);
            return;
        }

        courseStudent.IsCurrentlyAssigned = false;
        await context.SaveChangesAsync();
    }
}