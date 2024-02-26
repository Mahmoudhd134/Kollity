using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.DeleteAnswer;

public class DeleteAssignmentAnswerCommandHandler : ICommandHandler<DeleteAssignmentAnswerCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;
    private readonly IFileAccessor _fileAccessor;

    public DeleteAssignmentAnswerCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        IFileAccessor fileAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _fileAccessor = fileAccessor;
    }

    public async Task<Result> Handle(DeleteAssignmentAnswerCommand request, CancellationToken cancellationToken)
    {
        Guid assignmentId = request.AssignmentId,
            userId = _userAccessor.GetCurrentUserId();

        var assignment = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .Select(x => new { x.Mode, x.RoomId })
            .FirstOrDefaultAsync(cancellationToken);

        if (assignment == null)
            return AssignmentErrors.NotFound(assignmentId);

        string filePath;
        if (assignment.Mode == AssignmentMode.Individual)
        {
            filePath = await _context.AssignmentAnswers
                .Where(x => x.AssignmentId == assignmentId && x.StudentId == userId)
                .Select(x => x.File)
                .FirstOrDefaultAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(filePath))
                return AssignmentErrors.AnswerNotFound;
            var result = await _context.AssignmentAnswers
                .Where(x => x.AssignmentId == assignmentId && x.StudentId == userId)
                .ExecuteDeleteAsync(cancellationToken);
            if (result == 0)
                return Error.UnKnown;
            await _fileAccessor.Delete(filePath);
            return Result.Success();
        }

        var groupId = await _context.AssignmentGroups
            .Where(x => x.RoomId == assignment.RoomId)
            .Where(x => x.AssignmentGroupsStudents.Any(xx => xx.StudentId == userId))
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (groupId == default)
            return AssignmentErrors.UserIsNotInAnyGroup;

        filePath = await _context.AssignmentAnswers
            .Where(x => x.AssignmentId == assignmentId && x.AssignmentGroupId == groupId)
            .Select(x => x.File)
            .FirstOrDefaultAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(filePath))
            return AssignmentErrors.AnswerNotFound;
        var result1 = await _context.AssignmentAnswers
            .Where(x => x.AssignmentId == assignmentId && x.AssignmentGroupId == groupId)
            .ExecuteDeleteAsync(cancellationToken);
        if (result1 == 0)
            return Error.UnKnown;
        await _fileAccessor.Delete(filePath);
        return Result.Success();
    }
}