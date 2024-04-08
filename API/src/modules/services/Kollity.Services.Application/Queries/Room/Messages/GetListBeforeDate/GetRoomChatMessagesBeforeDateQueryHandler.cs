using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Abstractions.Services;
using Kollity.Services.Application.Dtos.Room.Message;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Room.Messages.GetListBeforeDate;

public class
    GetRoomChatMessagesBeforeDateQueryHandler : IQueryHandler<GetRoomChatMessagesBeforeDateQuery,
    List<RoomChatMessageDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public GetRoomChatMessagesBeforeDateQueryHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result<List<RoomChatMessageDto>>> Handle(GetRoomChatMessagesBeforeDateQuery request,
        CancellationToken cancellationToken)
    {
        var userId = _userServices.GetCurrentUserId();
        var isJoined = await _context.UserRooms
            .AnyAsync(x => x.RoomId == request.RoomId && x.UserId == userId && x.JoinRequestAccepted,
                cancellationToken);

        if (isJoined == false)
            return RoomErrors.UserIsNotJoined(userId);
        var roomMessageDtos = await _context.RoomMessages
            .Where(m => m.RoomId == request.RoomId && m.Date < request.Date.ToUniversalTime())
            .OrderByDescending(m => m.Date)
            .Take(100)
            .Select(x => new RoomChatMessageDto
            {
                Id = x.Id,
                Text = x.Text,
                SentAt = x.Date,
                FileName = x.File != null ? x.File.FileName : null,
                Sender = x.SenderId != null
                    ? new RoomChatMessageSender
                    {
                        Id = x.Sender.Id,
                        UserName = x.Sender.UserName,
                        Image = x.Sender.ProfileImage
                    }
                    : null
            })
            .Reverse()
            .ToListAsync(cancellationToken);

        return roomMessageDtos;
    }
}