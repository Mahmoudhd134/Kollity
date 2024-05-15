using Kollity.Services.Application.Dtos.Room.Message;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Room.Messages.GetPinnedBeforeData;

public class
    GetPinnedRoomChatMessagesBeforeDateQueryHandler(ApplicationDbContext context, IUserServices userServices)
    : IQueryHandler<GetPinnedRoomChatMessagesBeforeDateQuery,
        List<RoomChatMessageDto>>
{
    public async Task<Result<List<RoomChatMessageDto>>> Handle(GetPinnedRoomChatMessagesBeforeDateQuery request,
        CancellationToken cancellationToken)
    {
        var userId = userServices.GetCurrentUserId();
        var isJoined = await context.UserRooms
            .AnyAsync(x => x.RoomId == request.RoomId && x.UserId == userId && x.JoinRequestAccepted,
                cancellationToken);

        if (isJoined == false)
            return RoomErrors.UserIsNotJoined(userId);

        var utcDate = request.Date.ToUniversalTime();
        var roomMessageDtos = await context.RoomMessages
            .Where(m => m.RoomId == request.RoomId && m.IsPinned && m.Date < utcDate)
            .OrderByDescending(m => m.Date)
            .Take(request.Count)
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