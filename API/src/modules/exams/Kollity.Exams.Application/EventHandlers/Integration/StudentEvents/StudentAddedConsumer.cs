using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Domain;
using Kollity.Services.Contracts.Student;

namespace Kollity.Exams.Application.EventHandlers.Integration.StudentEvents;

public class StudentAddedConsumer(ExamsDbContext context) : IntegrationEventConsumer<StudentAddedIntegrationEvent>
{
    protected override Task Handle(StudentAddedIntegrationEvent integrationEvent)
    {
        var student = new User
        {
            Id = integrationEvent.Id,
            UserName = integrationEvent.UserName,
            Code = integrationEvent.Code,
            FullName = integrationEvent.FullName,
            IsDeleted = false,
        };
        context.Users.Add(student);
        return context.SaveChangesAsync();
    }
}