using Kollity.Services.Application.Abstractions;
using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.Domain.ErrorHandlers.Errors;
using Kollity.Services.Domain.RoomModels;
using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Abstractions.Files;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Abstractions.Services;
using Kollity.Services.Application.Events.RoomContent;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.AddContent;

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
        _eventCollection.Raise(new RoomContentAddedEvent(content));
        return Result.Success();
    }
}