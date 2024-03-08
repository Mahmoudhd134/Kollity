using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Group.AcceptInvitation;

public class AcceptAssignmentGroupInvitationCommandHandler : ICommandHandler<AcceptAssignmentGroupInvitationCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public AcceptAssignmentGroupInvitationCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public AcceptAssignmentGroupInvitationCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(AcceptAssignmentGroupInvitationCommand request,
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
            .ExecuteUpdateAsync(c =>
                c.SetProperty(x => x.JoinRequestAccepted, true), cancellationToken);

        await _context.AssignmentGroupStudents
            .Where(x => x.StudentId == userId && x.JoinRequestAccepted == false)
            .ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }
}