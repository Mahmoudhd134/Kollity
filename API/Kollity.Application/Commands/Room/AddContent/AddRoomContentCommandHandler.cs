using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.AddContent;

public class AddRoomContentCommandHandler : ICommandHandler<AddRoomContentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileAccessor _fileAccessor;
    private readonly IUserAccessor _userAccessor;

    public AddRoomContentCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        IFileAccessor fileAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _fileAccessor = fileAccessor;
    }

    public async Task<Result> Handle(AddRoomContentCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userAccessor.GetCurrentUserId(),
            roomId = request.RoomId;
        var isSupervisor = await _context.UserRooms
            .AnyAsync(x =>
                    x.RoomId == roomId &&
                    x.UserId == userId &&
                    x.IsSupervisor == true,
                cancellationToken);
        if (isSupervisor == false)
            return RoomErrors.UnAuthorizeAddContent;

        var path = await _fileAccessor.UploadFile(request.AddRoomContentDto.File, Category.RoomContent);
        var content = new RoomContent
        {
            Name = request.AddRoomContentDto.Name,
            RoomId = roomId,
            UploaderId = userId,
            FilePath = path,
            UploadTime = DateTime.UtcNow
        };

        _context.RoomContents.Add(content);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}