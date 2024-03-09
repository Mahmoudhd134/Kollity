using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Application.Abstractions.Services;
using Kollity.Contracts.Dto;
using Kollity.Contracts.Events.Content;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.AddContent;

public class AddRoomContentCommandHandler : ICommandHandler<AddRoomContentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public AddRoomContentCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices, EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(AddRoomContentCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            roomId = request.RoomId;
        var roomName = await _context.UserRooms
            .Where(x =>
                x.RoomId == roomId &&
                x.UserId == userId &&
                x.IsSupervisor == true)
            .Select(x => x.Room.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(roomName))
            return RoomErrors.UnAuthorizeAddContent;

        var path = await _fileServices.UploadFile(request.AddRoomContentDto.File, Category.RoomContent);
        var addedAt = DateTime.UtcNow;
        var content = new RoomContent
        {
            Name = request.AddRoomContentDto.Name,
            RoomId = roomId,
            UploaderId = userId,
            FilePath = path,
            UploadTime = addedAt
        };

        _context.RoomContents.Add(content);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new RoomContentAddedEvent(
            roomId,
            roomName,
            addedAt,
            content.Id,
            content.Name,
            await _context.UserRooms
                .Where(x => x.RoomId == roomId)
                .Select(x => x.User)
                .Where(x => x.EmailConfirmed && x.EnabledEmailNotifications)
                .Select(x => new UserEmailDto
                {
                    FullName = x.FullNameInArabic,
                    Email = x.Email
                })
                .ToListAsync(cancellationToken)
        ));
        return Result.Success();
    }
}