﻿using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.AddContent;

public class AddRoomContentCommandHandler : ICommandHandler<AddRoomContentCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IFileAccessor _fileAccessor;
    private readonly IUserAccessor _userAccessor;

    public AddRoomContentCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        IFileAccessor fileAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _fileAccessor = fileAccessor;
=======
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;

    public AddRoomContentCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(AddRoomContentCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        Guid userId = _userAccessor.GetCurrentUserId(),
=======
        Guid userId = _userServices.GetCurrentUserId(),
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
            roomId = request.RoomId;
        var isSupervisor = await _context.UserRooms
            .AnyAsync(x =>
                    x.RoomId == roomId &&
                    x.UserId == userId &&
                    x.IsSupervisor == true,
                cancellationToken);
        if (isSupervisor == false)
            return RoomErrors.UnAuthorizeAddContent;

<<<<<<< HEAD
        var path = await _fileAccessor.UploadFile(request.AddRoomContentDto.File, Category.RoomContent);
=======
        var path = await _fileServices.UploadFile(request.AddRoomContentDto.File, Category.RoomContent);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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