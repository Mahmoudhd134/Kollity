using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Exceptions;
using Kollity.Services.Contracts.Student;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.EventHandlers.Integration.StudentEvents;

public class StudentEditedConsumer(ExamsDbContext context) : IntegrationEventConsumer<StudentEditedIntegrationEvent>
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