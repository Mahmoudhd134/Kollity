using Kollity.Application.Dtos.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Events.Assignment.Created;

public class AssignmentCreatedEventHandlerSendMailsHandler : IEventHandler<AssignmentCreatedEvent>
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public AssignmentCreatedEventHandlerSendMailsHandler(ApplicationDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task Handle(AssignmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments
            .Where(x => x.Id == notification.AssignmentId)
            .Select(x => new
            {
                AssignmentName = x.Name,
                OpenUntil = x.OpenUntilDate,
                RoomName = x.Room.Name,
                RoomDoctorId = x.Room.DoctorId,
                CourseName = x.Room.Course.Name,
                Users = x.Room.UsersRooms
                    .Where(xx => xx.UserId != x.DoctorId)
                    .Where(xx => xx.User.EmailConfirmed && xx.User.EnabledEmailNotifications)
                    .Select(xx => new UserEmailDto()
                    {
                        Email = xx.User.Email,
                        FullName = xx.User.FullNameInArabic
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
        if (assignment is null)
            return;

        await _emailService.TrySendNewAssignmentEmailAsync(assignment.Users, assignment.RoomName, assignment.CourseName,
            assignment.AssignmentName, assignment.OpenUntil);
    }
}