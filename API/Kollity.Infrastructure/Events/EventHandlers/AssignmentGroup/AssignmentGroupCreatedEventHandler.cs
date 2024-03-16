using Kollity.Application.IntegrationEvents.AssignmentGroup;
using Kollity.Application.IntegrationEvents.Dto;
using Kollity.Infrastructure.Abstraction;
using Kollity.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Infrastructure.Events.EventHandlers.AssignmentGroup;

public class AssignmentGroupCreatedEventHandler : IEventHandler<AssignmentGroupCreatedEvent>
{
    private readonly IEmailService _emailService;
    private readonly ApplicationDbContext _context;

    public AssignmentGroupCreatedEventHandler(IEmailService emailService, ApplicationDbContext context)
    {
        _emailService = emailService;
        _context = context;
    }

    public async Task Handle(AssignmentGroupCreatedEvent notification, CancellationToken cancellationToken)
    {
        var dto = notification.AssignmentGroupDto;

        var room = await _context.Rooms
            .Where(x => x.Id == dto.RoomId)
            .Select(x => new
            {
                RoomName = x.Name,
                CourseName = x.Course.Name
            })
            .FirstOrDefaultAsync(cancellationToken);
        if (room is null)
            return;

        var userIds = dto.Members
            .Where(x => x.IsJoined == false)
            .Select(x => x.Id)
            .ToList();

        var users = await _context.Users
            .Where(x => userIds.Contains(x.Id))
            .Where(x => x.EmailConfirmed && x.EnabledEmailNotifications)
            .Select(x => new UserEmailDto(x.Email, x.FullNameInArabic))
            .ToListAsync(cancellationToken);


        var creator = dto.Members.First(x => x.IsJoined);
        await _emailService.TrySendAssignmentGroupInvitationEmailsAsync(
            users,
            creator.FullName,
            room.RoomName,
            room.CourseName,
            dto.Code);
    }
}