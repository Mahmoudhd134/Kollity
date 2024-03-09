using Kollity.Contracts.Dto;
using Kollity.Contracts.Events.AssignmentGroup;
using Kollity.NotificationServices.Abstraction;

namespace Kollity.NotificationServices.EventHandlers.AssignmentGroup;

public class AssignmentGroupCreatedEventHandler : IEventHandler<AssignmentGroupCreatedEvent>
{
    private readonly IEmailService _emailService;

    public AssignmentGroupCreatedEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public Task Handle(AssignmentGroupCreatedEvent notification, CancellationToken cancellationToken)
    {
        var (dto, _, roomName, courseName) = notification;

        var users = dto.Members
            .Where(x => x.IsJoined == false)
            .Select(x => new UserEmailDto()
            {
                Email = x.Email,
                FullName = x.FullName
            })
            .ToList();

        var creator = dto.Members.First(x => x.IsJoined);
        return _emailService.TrySendAssignmentGroupInvitationEmailsAsync(
            users,
            creator.FullName,
            roomName,
            courseName,
            dto.Code);
    }
}