using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Group.DeleteStudent;

public class DeleteStudentFromAssignmentGroupCommandHandler : ICommandHandler<DeleteStudentFromAssignmentGroupCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public DeleteStudentFromAssignmentGroupCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public DeleteStudentFromAssignmentGroupCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(DeleteStudentFromAssignmentGroupCommand request,
        CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        Guid userId = _userAccessor.GetCurrentUserId(),
=======
        Guid userId = _userServices.GetCurrentUserId(),
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
            groupId = request.InvitationDto.GroupId,
            studentId = request.InvitationDto.StudentId;

        var roomOperationsState = await _context.Rooms
            .Where(x => x.AssignmentGroups.Any(xx => xx.Id == groupId))
            .Select(x => x.AssignmentGroupOperationsEnabled)
            .FirstOrDefaultAsync(cancellationToken);

        if (roomOperationsState == false)
            return AssignmentErrors.OperationIsOff;


        //check user is in the group
        var isJoined = await _context.AssignmentGroupStudents
            .AnyAsync(x => x.StudentId == userId && x.AssignmentGroupId == groupId && x.JoinRequestAccepted,
                cancellationToken);
        if (isJoined == false)
            return AssignmentErrors.UserIsNotInTheGroup;

        await _context.AssignmentGroupStudents
            .Where(x => x.StudentId == studentId && x.AssignmentGroupId == groupId && x.JoinRequestAccepted)
            .ExecuteDeleteAsync(cancellationToken);
        return Result.Success();
    }
}