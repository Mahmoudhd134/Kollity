using Kollity.Application.Dtos.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Events.AssignmentGroup.Created;

public class AssignmentGroupCreatedEventHandlerSendNotificationsHandler : IEventHandler<AssignmentGroupCreatedEvent>
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public AssignmentGroupCreatedEventHandlerSendNotificationsHandler(ApplicationDbContext context,
        IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task Handle(AssignmentGroupCreatedEvent notification, CancellationToken cancellationToken)
    {
        var (dto, roomName, courseName) = notification;
        var ids = dto.Members
            .Where(x => x.IsJoined == false)
            .Select(x => x.Id)
            .ToList();

        var users = await _context.Users
            .Where(x => string.IsNullOrWhiteSpace(x.Email) == false && x.EmailConfirmed)
            .Where(x => x.EnabledEmailNotifications)
            .Where(x => ids.Contains(x.Id))
            .Select(x => new UserEmailDto()
            {
                Email = x.Email,
                FullName = x.FullNameInArabic
            })
            .ToListAsync(cancellationToken);


        var creator = dto.Members.First(x => x.IsJoined);
        await _emailService.TrySendAssignmentGroupInvitationEmailsAsync(
            users,
            creator.FullName,
            roomName,
            courseName,
            dto.Code);
    }
}