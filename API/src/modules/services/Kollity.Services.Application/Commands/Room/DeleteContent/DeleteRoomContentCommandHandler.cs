using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.DeleteContent;

public class DeleteRoomContentCommandHandler : ICommandHandler<DeleteRoomContentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public DeleteRoomContentCommandHandler(ApplicationDbContext context, IFileServices fileServices,
        IUserServices userServices, EventCollection eventCollection)
    {
        _context = context;
        _fileServices = fileServices;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeleteRoomContentCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            roomId = request.RoomId,
            contentId = request.Id;

        var isSupervisor = await _context.UserRooms
            .AnyAsync(x =>
                    x.RoomId == roomId &&
                    x.UserId == userId &&
                    x.IsSupervisor == true,
                cancellationToken);
        if (isSupervisor == false)
            return RoomErrors.UnAuthorizeDeleteContent;

        var roomContent = await _context.RoomContents
            .Where(x => x.Id == contentId)
            .FirstOrDefaultAsync(cancellationToken);

        if (roomContent is null)
            return RoomErrors.ContentIdNotFound(contentId);

        await _fileServices.Delete(roomContent.FilePath);

        _context.RoomContents.Remove(roomContent);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new RoomContentDeletedEvent(roomContent));

        return Result.Success();
    }
}