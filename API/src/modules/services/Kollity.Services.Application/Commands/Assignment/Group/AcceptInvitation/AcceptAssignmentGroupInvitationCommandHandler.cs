using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Group.AcceptInvitation;

public class AcceptAssignmentGroupInvitationCommandHandler : ICommandHandler<AcceptAssignmentGroupInvitationCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public AcceptAssignmentGroupInvitationCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result> Handle(AcceptAssignmentGroupInvitationCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            groupId = request.GroupId;

        await _context.AssignmentGroupStudents
            .Where(x => x.AssignmentGroupId == groupId && x.StudentId == userId)
            .ExecuteUpdateAsync(c =>
                c.SetProperty(x => x.JoinRequestAccepted, true), cancellationToken);

        await _context.AssignmentGroupStudents
            .Where(x => x.StudentId == userId && x.JoinRequestAccepted == false)
            .ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }
}