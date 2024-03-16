using Kollity.Application.IntegrationEvents.AssignmentGroup;
using Kollity.Application.IntegrationEvents.Dto;
using Kollity.Infrastructure.Abstraction;
using Kollity.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Infrastructure.Events.EventHandlers.AssignmentGroup;

public class
    AssignmentGroupInvitationSentEventHandler : IEventHandler<AssignmentGroupInvitationSentEvent>
{
    private readonly IEmailService _emailService;
    private readonly ApplicationDbContext _context;

    public AssignmentGroupInvitationSentEventHandler(IEmailService emailService, ApplicationDbContext context)
    {
        _emailService = emailService;
        _context = context;
    }

    public async Task Handle(AssignmentGroupInvitationSentEvent notification, CancellationToken cancellationToken)
    {
        var dto = await _context.AssignmentGroupStudents
            .Where(x => x.Id == notification.AssignmentGroupStudent.Id)
            .AsSplitQuery()
            .Select(x => new
            {
                x.Student.EmailConfirmed,
                x.Student.EnabledEmailNotifications,
                x.Student.Email,
                x.Student.FullNameInArabic,
                x.AssignmentGroup.Code,
                RoomName = x.AssignmentGroup.Room.Name,
                CourseName = x.AssignmentGroup.Room.Course.Name
            })
            .Where(x => x.EmailConfirmed && x.EnabledEmailNotifications)
            .FirstOrDefaultAsync(cancellationToken);

        if (dto is null)
            return;

        await _emailService.TrySendAssignmentGroupInvitationEmailsAsync(
            [new UserEmailDto(dto.Email, dto.FullNameInArabic)],
            null,
            dto.RoomName,
            dto.CourseName,
            dto.Code);
    }
}