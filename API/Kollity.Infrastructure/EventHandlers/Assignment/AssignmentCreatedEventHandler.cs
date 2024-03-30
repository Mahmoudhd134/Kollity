using Kollity.Application.IntegrationEvents.Assignment;
using Kollity.Application.IntegrationEvents.Dto;
using Kollity.Infrastructure.Abstraction.Email;
using Kollity.Infrastructure.Abstraction.Events;
using Kollity.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Infrastructure.EventHandlers.Assignment;

public class AssignmentCreatedEventHandler : IEventHandler<AssignmentCreatedEvent>
{
    private readonly IEmailService _emailService;
    private readonly ApplicationDbContext _context;

    public AssignmentCreatedEventHandler(IEmailService emailService, ApplicationDbContext context)
    {
        _emailService = emailService;
        _context = context;
    }

    public async Task Handle(AssignmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms
            .Where(x => x.Id == notification.Assignment.RoomId)
            .Select(x => new
            {
                RoomName = x.Name,
                CourseName = x.Course.Name,
                Students = x.UsersRooms
                    .Where(xx => xx.UserId != x.DoctorId)
                    .Select(xx => xx.User)
                    .Where(xx => xx.EmailConfirmed && xx.EnabledEmailNotifications)
                    .Select(xx => new UserEmailDto(xx.Email, xx.FullNameInArabic))
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (room is null)
            return;

        await _emailService.TrySendNewAssignmentEmailAsync(
            room.Students,
            room.RoomName,
            room.CourseName,
            notification.Assignment.Name,
            notification.Assignment.OpenUntilDate);
    }
}