using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Doctor;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.EventHandlers.Integration.DoctorEvents;

public class DoctorDeletedConsumer(FeedbackDbContext context)
    : IntegrationEventConsumer<DoctorDeletedIntegrationEvent>
{
    protected override async Task Handle(DoctorDeletedIntegrationEvent integrationEvent)
    {
        var result = await context.Users
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsDeleted, true));

        if (result == 0)
            throw new UserExceptions.DoctorNotFound(integrationEvent.Id);
    }
}