using Kollity.Services.Application.Dtos.Room.Message;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Messages.Pin;

public class PinRoomChatMessageCommandHandler(ApplicationDbContext context, IUserServices userServices)
    : ICommandHandler<PinRoomChatMessageCommand, RoomChatMessageDto>
{
    public async Task<Result<RoomChatMessageDto>> Handle(PinRoomChatMessageCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId = userServices.GetCurrentUserId(),
            roomId = request.RoomId,
            messageId = request.MessageId;

        var message = await context.RoomMessages
            .FirstOrDefaultAsync(x => x.RoomId == roomId && x.Id == messageId, cancellationToken);
        if (message is null)
            return RoomErrors.MessageNotFound(messageId);

        var isUserInRoom = await context.UserRooms
            .AnyAsync(x => x.UserId == userId && x.RoomId == roomId && x.JoinRequestAccepted, cancellationToken);
        if (isUserInRoom == false)
            return RoomErrors.UserIsNotJoined(userId);

        message.IsPinned = true;
        await context.SaveChangesAsync(cancellationToken);
        var sender = await context.Users
            .Where(x => x.Id == message.SenderId)
            .Select(x => new RoomChatMessageSenderDto
            {
                Id = x.Id,
                UserName = x.UserName,
                Image = x.ProfileImage
            })
            .FirstOrDefaultAsync(cancellationToken);
        var dto = new RoomChatMessageDto
        {
            Id = message.Id,
            Text = message.Text,
            IsRead = message.IsRead,
            SentAt = message.Date,
            FileName = message.File?.FileName,
            Poll = message.Poll != null
                ? new ChatPollDto
                {
                    Question = message.Poll.Question,
                    Options = message.Poll.Options.Select(xx => new ChatPollOptionDto { Option = xx }).ToList(),
                    MaxOptionsCountForSubmission = message.Poll.MaxOptionsCountForSubmission,
                    IsMultiAnswer = message.Poll.IsMultiAnswer
                }
                : null,
            Type = message.Type,
            SenderDto = sender
        };
        return dto;
    }
}