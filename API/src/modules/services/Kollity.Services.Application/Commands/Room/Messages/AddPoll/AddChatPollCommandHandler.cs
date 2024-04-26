using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Abstractions.RealTime;
using Kollity.Services.Application.Dtos.Room.Message;
using Kollity.Services.Application.Events.Room.Messages;
using Kollity.Services.Domain.Errors;
using Kollity.Services.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Messages.AddPoll;

public class AddChatPollCommandHandler(
    EventCollection eventCollection,
    IUserServices userServices,
    ApplicationDbContext context,
    IRoomConnectionsServices roomConnectionsServices) : ICommandHandler<AddChatPollCommand, RoomChatMessageDto>
{
    public async Task<Result<RoomChatMessageDto>> Handle(AddChatPollCommand request,
        CancellationToken cancellationToken)
    {
        var poll = request.AddChatPollDto;
        var errors = new List<Error>();

        if (poll.Question?.Length > 500)
        {
            errors.Add(RoomErrors.PollInvalidQuestionLength);
        }

        if (poll.Options.Count is < 0 or > 10)
        {
            errors.Add(RoomErrors.PollInvalidOptionsCount);
        }

        if (poll.Options.Any(option => option.Length > 300))
        {
            errors.Add(RoomErrors.PollInvalidOptionLength);
        }

        if (errors.Count > 0)
            return errors;

        Guid userId = userServices.GetCurrentUserId(),
            roomId = request.RoomId;

        var isJoined = await context.UserRooms
            .AnyAsync(x => x.UserId == userId && x.RoomId == roomId && x.JoinRequestAccepted, cancellationToken);
        if (isJoined == false)
            return RoomErrors.UserIsNotJoined(userId);

        var message = new RoomMessage
        {
            RoomId = roomId,
            SenderId = userId,
            Date = DateTime.UtcNow,
            IsRead = roomConnectionsServices.GetUsersConnectedToRoom(roomId).Count > 1,
            Poll = new MessagePoll
            {
                Question = request.AddChatPollDto.Question,
                Options = request.AddChatPollDto.Options
            },
            Type = RoomMessageType.Poll
        };

        context.RoomMessages.Add(message);
        var result = await context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        eventCollection.Raise(new RoomChatMessageAddedEvent(message));

        var sender = await context.Users
            .Where(x => x.Id == userId)
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
            SentAt = message.Date,
            IsRead = message.IsRead,
            SenderDto = sender,
            Poll = new ChatPollDto
            {
                Options = message.Poll.Options.Select(x => new ChatPollOptionDto
                {
                    Option = x,
                    Count = 0
                }).ToList(),
                Question = message.Poll.Question,
            },
            Type = message.Type
        };

        return dto;
    }
}