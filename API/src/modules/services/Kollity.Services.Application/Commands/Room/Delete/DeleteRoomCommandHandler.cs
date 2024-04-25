using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Domain.Errors;
using Kollity.Services.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Delete;

public class DeleteRoomCommandHandler : ICommandHandler<DeleteRoomCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IFileServices _fileServices;
    private readonly EventCollection _eventCollection;

    public DeleteRoomCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices, EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == request.RoomId, cancellationToken);
        if (room is null)
            return RoomErrors.NotFound(request.RoomId);

        var isAdmin = _userServices.GetCurrentUserRoles().Contains(Role.Admin);
        var id = _userServices.GetCurrentUserId();
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

        await _fileServices.Delete(content.Contents
            .Concat(content.AssignmentAnswers)
            .Concat(content.AssingmentFiles)
            .ToList());
        
        _eventCollection.Raise(new RoomDeletedEvent(room));

        return Result.Success();
    }
}