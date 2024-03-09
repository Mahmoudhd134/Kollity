using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Application.Abstractions.Services;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.DeleteContent;

public class DeleteRoomContentCommandHandler : ICommandHandler<DeleteRoomContentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;

    public DeleteRoomContentCommandHandler(ApplicationDbContext context, IFileServices fileServices,
        IUserServices userServices)
    {
        _context = context;
        _fileServices = fileServices;
        _userServices = userServices;
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

        var path = await _context.RoomContents
            .Where(x => x.Id == contentId)
            .Select(x => x.FilePath)
            .FirstOrDefaultAsync(cancellationToken);

        if (path is null)
            return RoomErrors.ContentIdNotFound(contentId);

        await _fileServices.Delete(path);
        var result = await _context.RoomContents
            .Where(x => x.Id == contentId)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}