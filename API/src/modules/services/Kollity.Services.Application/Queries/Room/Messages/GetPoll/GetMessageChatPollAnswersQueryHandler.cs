using Kollity.Services.Application.Dtos.Room.Message;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Room.Messages.GetPoll;

public class GetMessageChatPollAnswersQueryHandler(
    ApplicationDbContext context,
    IUserServices userServices
) : IQueryHandler<GetMessageChatPollAnswersQuery, ChatPollDto>
{
    public async Task<Result<ChatPollDto>> Handle(GetMessageChatPollAnswersQuery request,
        CancellationToken cancellationToken)
    {
        var userId = userServices.GetCurrentUserId();
        var poll = await context.RoomMessages
            .Where(x => x.Id == request.PollId)
            .Select(x => new
            {
                x.Poll,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (poll?.Poll is null)
            return RoomErrors.MessageNotFound(request.PollId);

        var answers = await context.RoomMessagePollAnswers
            .Where(x => x.PollId == request.PollId)
            .GroupBy(x => x.OptionIndex)
            .Select(x => new
            {
                Index = x.Key,
                Count = x.Count()
            })
            .ToDictionaryAsync(x => x.Index, cancellationToken);

        var userChooseOptions = await context.RoomMessagePollAnswers
            .Where(x => x.UserId == userId && x.PollId == request.PollId)
            .Select(x => x.OptionIndex)
            .ToListAsync(cancellationToken);

        return new ChatPollDto
        {
            Question = poll.Poll.Question,
            Options = poll.Poll.Options
                .Select((o, i) =>
                {
                    answers.TryGetValue((byte)i, out var answer);
                    return new ChatPollOptionDto
                    {
                        Option = o,
                        Count = answer?.Count ?? 0,
                        IsChoose = userChooseOptions.Contains((byte)i)
                    };
                })
                .ToList(),
            MaxOptionsCountForSubmission = poll.Poll.MaxOptionsCountForSubmission,
            IsMultiAnswer = poll.Poll.IsMultiAnswer
        };
    }
}