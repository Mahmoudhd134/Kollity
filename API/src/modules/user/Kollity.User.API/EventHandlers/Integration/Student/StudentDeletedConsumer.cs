using Kollity.Services.Contracts.Student;
using Kollity.User.API.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Kollity.User.API.EventHandlers.Integration.Student;

public class StudentDeletedConsumer(UserDbContext dbContext) : IConsumer<StudentDeletedIntegrationEvent>
{
    public Task Consume(ConsumeContext<StudentDeletedIntegrationEvent> context)
    {
        return dbContext.Users
            .Where(x => x.Id == context.Message.Id)
            .ExecuteDeleteAsync();
    }
}