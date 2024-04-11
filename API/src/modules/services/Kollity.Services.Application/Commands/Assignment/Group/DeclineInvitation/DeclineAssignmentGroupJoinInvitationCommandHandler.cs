using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Group.DeclineInvitation;

public class
    DeclineAssignmentGroupJoinInvitationCommandHandler : ICommandHandler<DeclineAssignmentGroupJoinInvitationCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public DeclineAssignmentGroupJoinInvitationCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result> Handle(DeclineAssignmentGroupJoinInvitationCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            groupId = request.GroupId;

        await _context.AssignmentGroupStudents
            .Where(x => x.AssignmentGroupId == groupId && x.StudentId == userId)
            .ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }
}