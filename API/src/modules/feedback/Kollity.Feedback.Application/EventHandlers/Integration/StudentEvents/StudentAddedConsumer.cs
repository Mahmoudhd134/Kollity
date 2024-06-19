using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Domain;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Student;

namespace Kollity.Feedback.Application.EventHandlers.Integration.StudentEvents;

public class StudentAddedConsumer(FeedbackDbContext context) : IntegrationEventConsumer<StudentAddedIntegrationEvent>
{
    protected override Task Handle(StudentAddedIntegrationEvent integrationEvent)
    {
        var student = new Domain.User
        {
            Id = integrationEvent.Id,
            UserName = integrationEvent.UserName,
            FullName = integrationEvent.FullName,
            UserType = UserType.Student,
            IsDeleted = false,
        };
        context.Users.Add(student);
        return context.SaveChangesAsync();
    }
}