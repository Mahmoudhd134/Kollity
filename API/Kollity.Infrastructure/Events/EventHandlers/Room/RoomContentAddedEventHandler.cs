using Kollity.Application.IntegrationEvents.Dto;
using Kollity.Application.IntegrationEvents.RoomContent;
using Kollity.Infrastructure.Abstraction;
using Kollity.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Infrastructure.Events.EventHandlers.Room;

public class RoomContentAddedEventHandler : IEventHandler<RoomContentAddedEvent>
{
    private readonly IEmailService _emailService;
    private readonly ApplicationDbContext _context;

    public RoomContentAddedEventHandler(IEmailService emailService, ApplicationDbContext context)
    {
        _emailService = emailService;
        _context = context;
    }

    public async Task Handle(RoomContentAddedEvent notification, CancellationToken cancellationToken)
    {
        var roomAndStudents = await _context.Rooms
            .Where(x => x.Id == notification.RoomContent.RoomId)
            .Select(x => new
            {
                RoomName = x.Name,
                Students = x.UsersRooms
                    .Where(xx => xx.UserId != x.DoctorId)
                    .Select(xx => xx.User)
                    .Where(xx => xx.EmailConfirmed && xx.EnabledEmailNotifications)
                    .Select(xx => new UserEmailDto(xx.Email, xx.FullNameInArabic))
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (roomAndStudents is null)
            return;

        await _emailService.TrySendRoomContentAddEmailAsync(
            roomAndStudents.RoomName,
            notification.RoomContent.Name,
            notification.RoomContent.UploadTime,
            roomAndStudents.Students
        );
    }
}