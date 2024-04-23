using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Domain.CourseModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kollity.Reporting.Application.EventHandlers.Integration.CourseEvents;

public class DoctorAssignedToCourseConsumer(ReportingDbContext context, ILogger<DoctorAssignedToCourseConsumer> logger)
    : IntegrationEventConsumer<DoctorAssignedToCourseIntegrationEvent>
{
    protected override async Task Handle(DoctorAssignedToCourseIntegrationEvent integrationEvent)
    {
        Guid dId = integrationEvent.DoctorId,
            cId = integrationEvent.CourseId;

        var doctorExists = await context.Doctors
            .AnyAsync(x => x.Id == dId);
        if (doctorExists == false)
        {
            logger.LogError("Assigning doctor with id {DoctorId} to course {CourseId}, but the doctor is not found",
                dId, cId);
            return;
        }

        var courseExists = await context.Courses
            .AnyAsync(x => x.Id == cId);
        if (courseExists == false)
        {
            logger.LogError("Assigning doctor with id {DoctorId} to course {CourseId}, but the course is not found",
                dId, cId);
            return;
        }

        var courseDoctorId = await context.CourseDoctorAndAssistants
            .Where(x => x.CourseId == cId && x.IsDoctor && x.IsCurrentlyAssigned)
            .Select(x => x.DoctorId)
            .FirstOrDefaultAsync();
        if (courseDoctorId != default)
        {
            logger.LogError(
                "Assigning doctor with id {DoctorId} to course {CourseId}, but the course already has a doctor assigned to it with id {CourseDoctorId}",
                dId, cId, courseDoctorId);
            return;
        }

        var courseDoctor = new CourseDoctorAndAssistants
        {
            CourseId = cId,
            DoctorId = dId,
            IsCurrentlyAssigned = true,
            IsDoctor = true,
            AssigningDate = integrationEvent.EventPublishedDateOnUtc
        };
        context.CourseDoctorAndAssistants.Add(courseDoctor);
        await context.SaveChangesAsync();
    }
}