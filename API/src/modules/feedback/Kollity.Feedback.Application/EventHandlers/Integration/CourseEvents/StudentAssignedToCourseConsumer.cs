using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Domain;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kollity.Feedback.Application.EventHandlers.Integration.CourseEvents;

public class StudentAssignedToCourseConsumer(
    FeedbackDbContext context,
    ILogger<StudentAssignedToCourseConsumer> logger)
    : IntegrationEventConsumer<StudentAssignedToCourseIntegrationEvent>
{
    protected override async Task Handle(StudentAssignedToCourseIntegrationEvent integrationEvent)
    {
        Guid sId = integrationEvent.StudentId,
            cId = integrationEvent.CourseId;

        var studentExists = await context.Users
            .AnyAsync(x => x.Id == sId);
        if (studentExists == false)
        {
            logger.LogError("Assigning student with id {Student} to course {CourseId}, but the student is not found",
                sId, cId);
            throw new UserExceptions.UserNotFound(sId);
        }

        var courseExists = await context.Courses
            .AnyAsync(x => x.Id == cId);
        if (courseExists == false)
        {
            logger.LogError("Assigning student with id {StudentId} to course {CourseId}, but the course is not found",
                sId, cId);
            throw new CourseExceptions.CourseNotFound(cId);
        }

        var studentAlreadyAssigned = await context.CourseStudents
            .AnyAsync(x => x.CourseId == cId && x.StudentId == sId);
        if (studentAlreadyAssigned)
        {
            logger.LogError(
                "Assigning student with id {StudentId} to course {CourseId}, but the student already assigned to the course",
                sId, cId);
            throw new CourseExceptions.StudentAlreadyAssigned(sId, cId);
        }

        var courseStudent = new CourseStudent
        {
            CourseId = cId,
            StudentId = sId,
        };
        context.CourseStudents.Add(courseStudent);
        await context.SaveChangesAsync();
    }
}