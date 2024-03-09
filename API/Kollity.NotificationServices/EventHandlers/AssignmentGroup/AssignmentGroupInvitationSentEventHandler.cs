using Kollity.Contracts.Events.AssignmentGroup;
using Kollity.NotificationServices.Abstraction;

namespace Kollity.NotificationServices.EventHandlers.AssignmentGroup;

public class
    AssignmentGroupInvitationSentEventHandler : IEventHandler<AssignmentGroupInvitationSentEvent>
{
    private readonly IEmailService _emailService;

    public AssignmentGroupInvitationSentEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public Task Handle(AssignmentGroupInvitationSentEvent notification, CancellationToken cancellationToken)
    {
        var invitation = notification.Dto;
        if (invitation.UserEmail is null)
            return Task.CompletedTask;

        return _emailService.TrySendAssignmentGroupInvitationEmailsAsync([invitation.UserEmail], null,
            invitation.RoomName, invitation.CourseName, invitation.GroupCode);
    }
}