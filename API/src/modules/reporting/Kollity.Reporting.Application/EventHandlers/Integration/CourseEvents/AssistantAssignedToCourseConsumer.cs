using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Application.Exceptions.Generic;
using Kollity.Reporting.Domain.CourseModels;
using Kollity.Reporting.Domain.UserModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kollity.Reporting.Application.EventHandlers.Integration.CourseEvents;

public class AssistantAssignedToCourseConsumer(
    ReportingDbContext context,
    ILogger<AssistantAssignedToCourseConsumer> logger)
    : IntegrationEventConsumer<AssistantAssignedToCourseIntegrationEvent>
{
    protected override async Task Handle(AssistantAssignedToCourseIntegrationEvent integrationEvent)
    {
        Guid aId = integrationEvent.AssistantId,
            cId = integrationEvent.CourseId;

        var assistantExists = await context.Doctors
            .AnyAsync(x => x.Id == aId && x.DoctorType == DoctorType.Assistant);
        if (assistantExists == false)
        {
            logger.LogError(
                "Assigning assistant with id {AssistantId} to course {CourseId}, but the assistant is not found",
                aId, cId);
            throw new UserExceptions.AssistantNotFound(aId);
        }

        var courseExists = await context.Courses
            .AnyAsync(x => x.Id == cId);
        if (courseExists == false)
        {
            logger.LogError(
                "Assigning assistant with id {AssistantId} to course {CourseId}, but the course is not found",
                aId, cId);
            throw new CourseExceptions.CourseNotFound(cId);
        }

        var assistantAlreadyAssigned = await context.CourseDoctorAndAssistants
            .AnyAsync(x => x.CourseId == cId && x.DoctorId == aId && x.IsDoctor == false && x.IsCurrentlyAssigned);
        if (assistantAlreadyAssigned)
        {
            logger.LogError(
                "Assigning assistant with id {Assistant} to course {CourseId}, but the course already has that assistant assigned to it",
                aId, cId);
            throw new CourseExceptions.AssistantAlreadyAssigned(aId, cId);
        }

        var courseAssistant = new CourseDoctorAndAssistants
        {
            CourseId = cId,
            DoctorId = aId,
            IsCurrentlyAssigned = true,
            IsDoctor = false,
            AssigningDate = integrationEvent.EventPublishedDateOnUtc
        };
        context.CourseDoctorAndAssistants.Add(courseAssistant);
        await context.SaveChangesAsync();
    }
}