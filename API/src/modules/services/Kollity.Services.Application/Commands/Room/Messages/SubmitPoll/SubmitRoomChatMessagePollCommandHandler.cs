using Kollity.Services.Domain.Errors;
using Kollity.Services.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Messages.SubmitPoll;

public class SubmitRoomChatMessagePollCommandHandler(
    ApplicationDbContext context,
    IUserServices userServices) : ICommandHandler<SubmitRoomChatMessagePollCommand>
{
    public async Task<Result> Handle(SubmitRoomChatMessagePollCommand request, CancellationToken cancellationToken)
    {
        Guid userId = userServices.GetCurrentUserId(),
            pollId = request.PollId;

        var poll = await context.RoomMessages
            .Where(x => x.Id == pollId)
            .Select(x => new
            {
                x.Poll,
                x.RoomId
            })
            .FirstOrDefaultAsync(cancellationToken);
        if (poll?.Poll is null)
            return RoomErrors.MessageNotFound(pollId);

        if (poll.Poll.Options.Count - 1 < request.OptionIndex)
            return RoomErrors.PollOptionNotFound;

        var isSubmittedBefore = await context.RoomMessagePollAnswers
            .AnyAsync(x => x.UserId == userId && x.PollId == pollId, cancellationToken);
        if (isSubmittedBefore)
            return RoomErrors.PollAlreadySubmitted;

        var isJoined = await context.UserRooms
            .AnyAsync(x => x.UserId == userId &&
                           x.RoomId == poll.RoomId &&
                           x.JoinRequestAccepted, cancellationToken);
        if (isJoined == false)
            return RoomErrors.UserIsNotJoined(userId);

        var answer = new MessagePollAnswer
        {
            PollId = pollId,
            UserId = userId,
            OptionIndex = request.OptionIndex
        };
        context.RoomMessagePollAnswers.Add(answer);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}