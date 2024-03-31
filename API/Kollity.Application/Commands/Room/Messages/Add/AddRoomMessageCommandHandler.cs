using Kollity.Application.Abstractions.Events;
using Kollity.Application.Abstractions.Files;
using Kollity.Application.Abstractions.RealTime;
using Kollity.Application.Dtos.Room.Message;
using Kollity.Application.IntegrationEvents.Room.Messages;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.Messages.Add;

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
            IsRead = _roomConnectionsServices.GetUsersConnectedToRoom(roomId).Count > 1
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

        await _context.SaveChangesAsync(cancellationToken);

        var sender = await _context.Users
            .Where(x => x.Id == userId)
            .Select(x => new RoomChatMessageSender
            {
                Id = x.Id,
                UserName = x.UserName,
                Image = x.ProfileImage
            })
            .FirstAsync(cancellationToken);

        _eventCollection.Raise(new RoomChatMessageAddedEvent(
            message,
            sender.Id,
            sender.UserName,
            sender.Image
        ));

        var messageDto = new RoomChatMessageDto
        {
            Id = message.Id,
            Text = message.Text,
            IsRead = message.IsRead,
            SentAt = message.Date,
            Sender = sender
        };

        return messageDto;
    }
}