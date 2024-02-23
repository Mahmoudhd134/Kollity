using Kollity.Application.Abstractions;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Group.AcceptInvitation;

public class AcceptAssignmentGroupInvitationCommandHandler : ICommandHandler<AcceptAssignmentGroupInvitationCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;

    public AcceptAssignmentGroupInvitationCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(AcceptAssignmentGroupInvitationCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userAccessor.GetCurrentUserId(),
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