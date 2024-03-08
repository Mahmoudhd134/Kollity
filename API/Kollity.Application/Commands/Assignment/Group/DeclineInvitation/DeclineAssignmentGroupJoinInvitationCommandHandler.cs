using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Group.DeclineInvitation;

public class
    DeclineAssignmentGroupJoinInvitationCommandHandler : ICommandHandler<DeclineAssignmentGroupJoinInvitationCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public DeclineAssignmentGroupJoinInvitationCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public DeclineAssignmentGroupJoinInvitationCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(DeclineAssignmentGroupJoinInvitationCommand request,
        CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        Guid userId = _userAccessor.GetCurrentUserId(),
=======
        Guid userId = _userServices.GetCurrentUserId(),
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
            groupId = request.GroupId;

        await _context.AssignmentGroupStudents
            .Where(x => x.AssignmentGroupId == groupId && x.StudentId == userId)
            .ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }
}