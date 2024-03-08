using Kollity.Application.Dtos.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Events.AssignmentGroup.InvitationSent;

public class
    AssignmentGroupInvitationSentEventHandlerSendEmailHandler : IEventHandler<AssignmentGroupInvitationSentEvent>
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public AssignmentGroupInvitationSentEventHandlerSendEmailHandler(ApplicationDbContext context,
        IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task Handle(AssignmentGroupInvitationSentEvent notification, CancellationToken cancellationToken)
    {
        var invitation = await _context.AssignmentGroupStudents
            .Where(x => x.Id == notification.InvitationId)
            .Where(x => x.Student.EmailConfirmed && x.Student.EnabledEmailNotifications)
            .Select(x => new
            {
                x.AssignmentGroup.Code,
                RoomName = x.AssignmentGroup.Room.Name,
                CourseName = x.AssignmentGroup.Room.Course.Name,
                UserEmail = new UserEmailDto()
                {
                    Email = x.Student.Email,
                    FullName = x.Student.FullNameInArabic
                },
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (invitation is null)
            return;

        await _emailService.TrySendAssignmentGroupInvitationEmailsAsync([invitation.UserEmail], null,
            invitation.RoomName, invitation.CourseName, invitation.Code);
    }
}