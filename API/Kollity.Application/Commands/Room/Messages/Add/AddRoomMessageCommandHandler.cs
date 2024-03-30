using Kollity.Application.Abstractions.Files;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.Messages.Add;

public class AddRoomMessageCommandHandler : ICommandHandler<AddRoomMessageCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IFileServices _fileServices;

    public AddRoomMessageCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
    }

    public async Task<Result> Handle(AddRoomMessageCommand request, CancellationToken cancellationToken)
    {
        Guid roomId = request.RoomId,
            userId = _userServices.GetCurrentUserId();
        var isJoined = await _context.UserRooms
            .AnyAsync(x => x.UserId == userId && x.RoomId == roomId && x.JoinRequestAccepted, cancellationToken);

        if (isJoined == false)
            return RoomErrors.UnAuthorizeAddMessage;

        var message = new RoomMessage()
        {
            Text = request.Dto.Text,
            SenderId = userId,
            RoomId = roomId,
            Date = DateTime.UtcNow,
            IsRead = false
        };
        if (request.Dto.File != null)
        {
            var path = await _fileServices.UploadFile(request.Dto.File, Category.RoomChatFile);
            message.File = new MessageFile()
            {
                FileName = request.Dto.File.FileName,
                FilePath = path
            };
        }

        return Result.Success();
    }
}