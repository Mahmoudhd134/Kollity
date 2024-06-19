using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Student;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.EventHandlers.Integration.StudentEvents;

public class StudentEditedConsumer(FeedbackDbContext context) : IntegrationEventConsumer<StudentEditedIntegrationEvent>
{
    protected override async Task Handle(StudentEditedIntegrationEvent integrationEvent)
    {
        var student = await context.Users
            .FirstOrDefaultAsync(s => s.Id == integrationEvent.Id);
        if (student is null)
            throw new UserExceptions.StudentNotFound(integrationEvent.Id);
        student.FullName = integrationEvent.FullName;
        student.UserName = integrationEvent.UserName;
        await context.SaveChangesAsync();
    }
}