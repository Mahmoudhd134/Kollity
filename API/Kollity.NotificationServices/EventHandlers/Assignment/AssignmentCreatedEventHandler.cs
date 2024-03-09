using Kollity.Contracts.Events.Assignment;
using Kollity.NotificationServices.Abstraction;

namespace Kollity.NotificationServices.EventHandlers.Assignment;

public class AssignmentCreatedEventHandler : IEventHandler<AssignmentCreatedEvent>
{
    private readonly IEmailService _emailService;

    public AssignmentCreatedEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public Task Handle(AssignmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var assignment = notification.EventDto;
        return _emailService.TrySendNewAssignmentEmailAsync(assignment.Users, assignment.RoomName, assignment.CourseName,
            assignment.AssignmentName, assignment.OpenUntil);
    }
}