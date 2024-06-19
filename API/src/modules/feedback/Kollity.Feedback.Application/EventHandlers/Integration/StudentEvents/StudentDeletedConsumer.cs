using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Student;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.EventHandlers.Integration.StudentEvents;

public class StudentDeletedConsumer(FeedbackDbContext context)
    : IntegrationEventConsumer<StudentDeletedIntegrationEvent>
{
    protected override async Task Handle(StudentDeletedIntegrationEvent integrationEvent)
    {
        var result = await context.Users
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsDeleted, true));
        if (result == 0)
            throw new UserExceptions.StudentNotFound(integrationEvent.Id);
    }
}