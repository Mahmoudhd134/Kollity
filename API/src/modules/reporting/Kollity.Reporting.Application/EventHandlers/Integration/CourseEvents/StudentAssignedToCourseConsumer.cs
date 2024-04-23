using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Domain.CourseModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kollity.Reporting.Application.EventHandlers.Integration.CourseEvents;

public class StudentAssignedToCourseConsumer(
    ReportingDbContext context,
    ILogger<StudentAssignedToCourseConsumer> logger)
    : IntegrationEventConsumer<StudentAssignedToCourseIntegrationEvent>
{
    protected override async Task Handle(StudentAssignedToCourseIntegrationEvent integrationEvent)
    {
        Guid sId = integrationEvent.StudentId,
            cId = integrationEvent.CourseId;

        var studentExists = await context.Students
            .AnyAsync(x => x.Id == sId);
        if (studentExists == false)
        {
            logger.LogError("Assigning student with id {Student} to course {CourseId}, but the student is not found",
                sId, cId);
            return;
        }

        var courseExists = await context.Courses
            .AnyAsync(x => x.Id == cId);
        if (courseExists == false)
        {
            logger.LogError("Assigning student with id {StudentId} to course {CourseId}, but the course is not found",
                sId, cId);
            return;
        }

        var studentAlreadyAssigned = await context.CourseStudents
            .AnyAsync(x => x.CourseId == cId && x.StudentId == sId && x.IsCurrentlyAssigned);
        if (studentAlreadyAssigned)
        {
            logger.LogError(
                "Assigning student with id {StudentId} to course {CourseId}, but the student already assigned to the course",
                sId, cId);
            return;
        }

        var courseStudent = new CourseStudent
        {
            CourseId = cId,
            StudentId = sId,
            IsCurrentlyAssigned = true,
            AssigningDate = integrationEvent.EventPublishedDateOnUtc
        };
        context.CourseStudents.Add(courseStudent);
        await context.SaveChangesAsync();
    }
}