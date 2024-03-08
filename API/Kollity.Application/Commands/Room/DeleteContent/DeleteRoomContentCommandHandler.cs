using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.DeleteContent;

public class DeleteRoomContentCommandHandler : ICommandHandler<DeleteRoomContentCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IFileAccessor _fileAccessor;
    private readonly IUserAccessor _userAccessor;

    public DeleteRoomContentCommandHandler(ApplicationDbContext context, IFileAccessor fileAccessor,
        IUserAccessor userAccessor)
    {
        _context = context;
        _fileAccessor = fileAccessor;
        _userAccessor = userAccessor;
=======
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;

    public DeleteRoomContentCommandHandler(ApplicationDbContext context, IFileServices fileServices,
        IUserServices userServices)
    {
        _context = context;
        _fileServices = fileServices;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(DeleteRoomContentCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        Guid userId = _userAccessor.GetCurrentUserId(),
=======
        Guid userId = _userServices.GetCurrentUserId(),
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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

<<<<<<< HEAD
        await _fileAccessor.Delete(path);
=======
        await _fileServices.Delete(path);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        var result = await _context.RoomContents
            .Where(x => x.Id == contentId)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}