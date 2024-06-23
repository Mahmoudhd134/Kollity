using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Exceptions;
using Kollity.Services.Contracts.Student;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.EventHandlers.Integration.StudentEvents;

public class StudentDeletedConsumer(ExamsDbContext context)
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