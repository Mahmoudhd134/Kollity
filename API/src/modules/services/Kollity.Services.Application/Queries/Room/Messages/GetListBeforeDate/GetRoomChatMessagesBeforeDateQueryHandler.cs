using Kollity.Services.Application.Dtos.Room.Message;
using Kollity.Services.Domain.Errors;
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


        var utcDate = request.Date.ToUniversalTime();
        var roomMessageDtos = await _context.RoomMessages
            .Where(m => m.RoomId == request.RoomId && m.Date < utcDate)
            .OrderByDescending(m => m.Date)
            .Take(100)
            .Select(x => new
            {
                x.Id,
                x.Text,
                x.IsRead,
                SentAt = x.Date,
                FileName = x.File != null ? x.File.FileName : null,
                SenderDto = x.SenderId != null
                    ? new RoomChatMessageSenderDto
                    {
                        Id = x.Sender.Id,
                        UserName = x.Sender.UserName,
                        Image = x.Sender.ProfileImage
                    }
                    : null,
                x.Poll,
                x.Type
            })
            .Reverse()
            .ToListAsync(cancellationToken);

        return roomMessageDtos.Select(x => new RoomChatMessageDto
        {
            Id = x.Id,
            Text = x.Text,
            IsRead = x.IsRead,
            SentAt = x.SentAt,
            SenderDto = x.SenderDto,
            FileName = x.FileName,
            Poll = x.Poll != null
                ? new ChatPollDto
                {
                    Question = x.Poll.Question,
                    Options = x.Poll.Options.Select(xx => new ChatPollOptionDto { Option = xx }).ToList(),
                    IsMultiAnswer = x.Poll.IsMultiAnswer,
                    MaxOptionsCountForSubmission = x.Poll.MaxOptionsCountForSubmission
                }
                : null,
            Type = x.Type
        }).ToList();
    }
}