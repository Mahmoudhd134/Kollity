using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kollity.Reporting.Application.EventHandlers.Integration.CourseEvents;

public class DoctorDeAssignedFromCourseConsumer(
    ReportingDbContext context,
    ILogger<DoctorDeAssignedFromCourseConsumer> logger)
    : IntegrationEventConsumer<DoctorDeAssignedFromCourseIntegrationEvent>
{
    protected override async Task Handle(DoctorDeAssignedFromCourseIntegrationEvent integrationEvent)
    {
        Guid dId = integrationEvent.DoctorId,
            cId = integrationEvent.CourseId;

        var courseDoctor = await context.CourseDoctorAndAssistants
            .Where(x =>
                x.CourseId == cId &&
                x.DoctorId == dId &&
                x.IsDoctor &&
                x.IsCurrentlyAssigned)
            .FirstOrDefaultAsync();

        if (courseDoctor == null)
        {
            logger.LogError(
                "Trying to de assign the doctor {DoctorId} from course {CourseId}, but the doctor is not assigned to the course",
                dId, cId);
            throw new CourseExceptions.DoctorIsNotAssigned(dId,cId);
        }

        courseDoctor.IsCurrentlyAssigned = false;
        await context.SaveChangesAsync();
    }
}