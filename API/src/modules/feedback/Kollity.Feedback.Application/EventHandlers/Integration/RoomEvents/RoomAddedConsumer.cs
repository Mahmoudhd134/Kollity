using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Domain;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Room;

namespace Kollity.Feedback.Application.EventHandlers.Integration.RoomEvents;

public class RoomAddedConsumer(FeedbackDbContext context) : IntegrationEventConsumer<RoomAddedIntegrationEvent>
{
    protected override Task Handle(RoomAddedIntegrationEvent integrationEvent)
    {
        var room = new Room
        {
            Id = integrationEvent.Id,
            Name = integrationEvent.Name,
            DoctorId = integrationEvent.DoctorId,
            CourseId = integrationEvent.CourseId,
            IsDeleted = false
        };

        context.Rooms.Add(room);
        return context.SaveChangesAsync();
    }
}