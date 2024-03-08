using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.Identity.Role;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.Delete;

public class DeleteRoomCommandHandler : ICommandHandler<DeleteRoomCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;
    private readonly IFileAccessor _fileAccessor;

    public DeleteRoomCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        IFileAccessor fileAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _fileAccessor = fileAccessor;
=======
    private readonly IUserServices _userServices;
    private readonly IFileServices _fileServices;

    public DeleteRoomCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == request.RoomId, cancellationToken);
        if (room is null)
            return RoomErrors.NotFound(request.RoomId);

<<<<<<< HEAD
        var isAdmin = _userAccessor.GetCurrentUserRoles().Contains(Role.Admin);
        var id = _userAccessor.GetCurrentUserId();
=======
        var isAdmin = _userServices.GetCurrentUserRoles().Contains(Role.Admin);
        var id = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        if (isAdmin == false && room.DoctorId != id)
            return RoomErrors.UnAuthorizeDelete;

        var content = await _context.Rooms
            .Where(x => x.Id == room.Id)
            .Select(x => new
            {
                Contents = x.RoomContents.Select(xx => xx.FilePath).ToList(),
                AssingmentFiles = x.Assignments.SelectMany(xx =>
                    xx.AssignmentFiles.Select(xxx => xxx.FilePath)).ToList(),
                AssignmentAnswers = x.Assignments.SelectMany(xx => 
                    xx.AssignmentsAnswers.Select(xxx => xxx.File)).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        _context.Rooms.Remove(room);

        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

<<<<<<< HEAD
        await _fileAccessor.Delete(content.Contents);
        await _fileAccessor.Delete(content.AssingmentFiles);
        await _fileAccessor.Delete(content.AssignmentAnswers);
=======
        await _fileServices.Delete(content.Contents);
        await _fileServices.Delete(content.AssingmentFiles);
        await _fileServices.Delete(content.AssignmentAnswers);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        return Result.Success();
    }
}