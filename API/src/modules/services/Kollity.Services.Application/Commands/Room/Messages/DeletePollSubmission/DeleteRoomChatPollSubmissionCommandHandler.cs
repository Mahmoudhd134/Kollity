using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Messages.DeletePollSubmission;

public class DeleteRoomChatPollSubmissionCommandHandler(ApplicationDbContext context, IUserServices userServices)
    : ICommandHandler<DeleteRoomChatPollSubmissionCommand>
{
    public async Task<Result> Handle(DeleteRoomChatPollSubmissionCommand request, CancellationToken cancellationToken)
    {
        var userId = userServices.GetCurrentUserId();
        var result = await context.RoomMessagePollAnswers
            .Where(x => x.PollId == request.PollId && x.UserId == userId && x.OptionIndex == request.OptionIndex)
            .ExecuteDeleteAsync(cancellationToken);
        return result > 0 ? Result.Success() : RoomErrors.PollSubmissionNotFound(request.PollId);
    }
}