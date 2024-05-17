using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Messages.UnPin;

public class UnPinRoomChatMessageCommandHandler(ApplicationDbContext context, IUserServices userServices)
    : ICommandHandler<UnPinRoomChatMessageCommand>
{
    public async Task<Result> Handle(UnPinRoomChatMessageCommand request, CancellationToken cancellationToken)
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

        message.IsPinned = false;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}