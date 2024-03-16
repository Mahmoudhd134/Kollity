using Kollity.Application.IntegrationEvents.Dto;
using Kollity.Application.IntegrationEvents.Exam;
using Kollity.Infrastructure.Abstraction;
using Kollity.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Infrastructure.Events.EventHandlers.Exam;

public class ExamAddedEventHandler : IEventHandler<ExamAddedEvent>
{
    private readonly IEmailService _emailService;
    private readonly ApplicationDbContext _context;

    public ExamAddedEventHandler(IEmailService emailService, ApplicationDbContext context)
    {
        _emailService = emailService;
        _context = context;
    }

    public async Task Handle(ExamAddedEvent notification, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms
            .Where(x => x.Id == notification.Exam.RoomId)
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

        await _emailService.TrySendExamAddedEmailAsync(
            notification.Exam.Name,
            notification.Exam.StartDate,
            room.RoomName,
            room.CourseName,
            room.Students
        );
    }
}