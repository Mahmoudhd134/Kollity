using Kollity.Services.Contracts.Doctor;
using Kollity.User.API.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Kollity.User.API.EventHandlers.Integration.Doctor;

public class DoctorDeletedConsumer(UserDbContext dbContext) : IConsumer<DoctorDeletedIntegrationEvent>
{
    public Task Consume(ConsumeContext<DoctorDeletedIntegrationEvent> context)
    {
        return dbContext.Users
            .Where(x => x.Id == context.Message.Id)
            .ExecuteDeleteAsync();
    }
}