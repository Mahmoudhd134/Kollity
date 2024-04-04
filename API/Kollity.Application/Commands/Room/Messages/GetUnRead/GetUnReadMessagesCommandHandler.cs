using Kollity.Application.Dtos.Room.Message;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.Messages.GetUnRead;

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
            .Select(x => new RoomChatMessageDto()
            {
                Id = x.Id,
                Text = x.Text,
                IsRead = x.IsRead,
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
            .ToListAsync(cancellationToken);

        var notReadIds = messages
            .Where(x => x.IsRead == false)
            .Select(x => x.Id)
            .ToList();
        await _context.RoomMessages
            .Where(x => notReadIds.Contains(x.Id))
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsRead, true), cancellationToken);

        return messages;
    }
}