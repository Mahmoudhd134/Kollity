using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.Messages.Delete;

public class DeleteRoomChatMessageCommandHandler : ICommandHandler<DeleteRoomChatMessageCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public DeleteRoomChatMessageCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result> Handle(DeleteRoomChatMessageCommand request, CancellationToken cancellationToken)
    {
        var userId = _userServices.GetCurrentUserId();
        var result = await _context.RoomMessages
            .Where(x => x.SenderId == userId && x.Id == request.MessageId)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0 ? Result.Success() : RoomErrors.MessageNotFound(request.MessageId);
    }
}