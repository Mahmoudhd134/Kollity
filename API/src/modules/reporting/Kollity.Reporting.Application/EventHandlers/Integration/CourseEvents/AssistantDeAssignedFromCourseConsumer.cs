using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kollity.Reporting.Application.EventHandlers.Integration.CourseEvents;

public class AssistantDeAssignedFromCourseConsumer(
    ReportingDbContext context,
    ILogger<AssistantDeAssignedFromCourseConsumer> logger)
    : IntegrationEventConsumer<AssistantDeAssignedFromCourseIntegrationEvent>
{
    protected override async Task Handle(AssistantDeAssignedFromCourseIntegrationEvent integrationEvent)
    {
        Guid aId = integrationEvent.AssistantId,
            cId = integrationEvent.CourseId;

        var courseAssistant = await context.CourseDoctorAndAssistants
            .Where(x =>
                x.CourseId == cId &&
                x.DoctorId == aId &&
                x.IsDoctor == false &&
                x.IsCurrentlyAssigned)
            .FirstOrDefaultAsync();

        if (courseAssistant == null)
        {
            logger.LogError(
                "Trying to de assign the assistant {AssistantId} from course {CourseId}, but the assistant is not assigned to the course",
                aId, cId);
            throw new CourseExceptions.AssistantIsNotAssigned(aId, cId);
        }

        courseAssistant.IsCurrentlyAssigned = false;
        await context.SaveChangesAsync();
    }
}