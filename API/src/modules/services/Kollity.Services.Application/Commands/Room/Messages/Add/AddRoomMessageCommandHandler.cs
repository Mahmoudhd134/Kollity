using Kollity.Services.Domain.RoomModels;
using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Abstractions.Files;
using Kollity.Services.Application.Abstractions.RealTime;
using Kollity.Services.Application.Dtos.Room.Message;
using Kollity.Services.Application.Events.Room.Messages;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Messages.Add;

public class AddRoomMessageCommandHandler : ICommandHandler<AddRoomMessageCommand, RoomChatMessageDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IFileServices _fileServices;
    private readonly EventCollection _eventCollection;
    private readonly IRoomConnectionsServices _roomConnectionsServices;

    public AddRoomMessageCommandHandler(
        ApplicationDbContext context,
        IUserServices userServices,
        IFileServices fileServices,
        EventCollection eventCollection,
        IRoomConnectionsServices roomConnectionsServices)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
        _eventCollection = eventCollection;
        _roomConnectionsServices = roomConnectionsServices;
    }

    public async Task<Result<RoomChatMessageDto>> Handle(AddRoomMessageCommand request,
        CancellationToken cancellationToken)
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
            IsRead = _roomConnectionsServices.GetUsersConnectedToRoom(roomId).Count > 1,
            Type = RoomMessageType.Text
        };

        if (request.Dto.File != null)
        {
            var path = await _fileServices.UploadFile(request.Dto.File, Category.RoomChatFile);
            message.File = new MessageFile()
            {
                FileName = request.Dto.File.FileName,
                FilePath = path
            };
            var ct = request.Dto.File.ContentType.ToLower();
            if (ct.StartsWith("video"))
                message.Type = RoomMessageType.Video;
            else if (ct.StartsWith("audio"))
                message.Type = RoomMessageType.Audio;
            else if (ct.StartsWith("image"))
                message.Type = RoomMessageType.Image;
            else
                message.Type = RoomMessageType.File;
        }

        _context.RoomMessages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);

        _eventCollection.Raise(new RoomChatMessageAddedEvent(message));

        var sender = await _context.Users
            .Where(x => x.Id == userId)
            .Select(x => new RoomChatMessageSenderDto
            {
                Id = x.Id,
                UserName = x.UserName,
                Image = x.ProfileImage
            })
            .FirstAsync(cancellationToken);


        var messageDto = new RoomChatMessageDto
        {
            Id = message.Id,
            Text = message.Text,
            IsRead = message.IsRead,
            SentAt = message.Date,
            SenderDto = sender,
            FileName = message.File?.FileName,
            Type = message.Type
        };

        return messageDto;
    }
}