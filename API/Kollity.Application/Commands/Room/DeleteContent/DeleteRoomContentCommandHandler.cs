using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.DeleteContent;

public class DeleteRoomContentCommandHandler : ICommandHandler<DeleteRoomContentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileAccessor _fileAccessor;
    private readonly IUserAccessor _userAccessor;

    public DeleteRoomContentCommandHandler(ApplicationDbContext context, IFileAccessor fileAccessor,
        IUserAccessor userAccessor)
    {
        _context = context;
        _fileAccessor = fileAccessor;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(DeleteRoomContentCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userAccessor.GetCurrentUserId(),
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

        var path = await _context.RoomContents
            .Where(x => x.Id == contentId)
            .Select(x => x.FilePath)
            .FirstOrDefaultAsync(cancellationToken);

        if (path is null)
            return RoomErrors.ContentIdNotFound(contentId);

        await _fileAccessor.Delete(path);
        var result = await _context.RoomContents
            .Where(x => x.Id == contentId)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}