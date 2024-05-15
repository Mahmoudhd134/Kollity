using Kollity.Services.Application.Dtos.Room.Message;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Messages.GetUnRead;

public class GetUnReadMessagesCommandHandler : ICommandHandler<GetUnReadMessagesCommand, List<RoomChatMessageDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public GetUnReadMessagesCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result<List<RoomChatMessageDto>>> Handle(GetUnReadMessagesCommand request,
        CancellationToken cancellationToken)
    {
        var userId = _userServices.GetCurrentUserId();

        var messages = await _context.RoomMessages
            .Where(x => x.RoomId == request.RoomId &&
                        x.Date > _context.UserRooms
                            .FirstOrDefault(ur => ur.UserId == userId && ur.RoomId == request.RoomId).LastOnlineDate)
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
            .ToListAsync(cancellationToken);

        var notReadIds = messages
            .Where(x => x.IsRead == false)
            .Select(x => x.Id)
            .ToList();
        await _context.RoomMessages
            .Where(x => notReadIds.Contains(x.Id))
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsRead, true), cancellationToken);

        return messages.Select(x => new RoomChatMessageDto
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
                    MaxOptionsCountForSubmission = x.Poll.MaxOptionsCountForSubmission,
                    IsMultiAnswer = x.Poll.IsMultiAnswer
                }
                : null,
            Type = x.Type
        }).ToList();
    }
}