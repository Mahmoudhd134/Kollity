using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Assignment;
using Kollity.Services.Domain.AssignmentModels;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.DeleteAnswer;

public class DeleteAssignmentAnswerCommandHandler : ICommandHandler<DeleteAssignmentAnswerCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IFileServices _fileServices;
    private readonly EventCollection _eventCollection;

    public DeleteAssignmentAnswerCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices, EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeleteAssignmentAnswerCommand request, CancellationToken cancellationToken)
    {
        Guid assignmentId = request.AssignmentId,
            userId = _userServices.GetCurrentUserId();

        var assignment = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .Select(x => new { x.Mode, x.RoomId })
            .FirstOrDefaultAsync(cancellationToken);

        if (assignment == null)
            return AssignmentErrors.NotFound(assignmentId);

        AssignmentAnswer answer;
        if (assignment.Mode == AssignmentMode.Individual)
        {
            answer = await _context.AssignmentAnswers
                .Where(x => x.AssignmentId == assignmentId && x.StudentId == userId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(answer.File))
                return AssignmentErrors.AnswerNotFound;
            var result = await _context.AssignmentAnswers
                .Where(x => x.AssignmentId == assignmentId && x.StudentId == userId)
                .ExecuteDeleteAsync(cancellationToken);
            if (result == 0)
                return Error.UnKnown;
            await _fileServices.Delete(answer.File);
            _eventCollection.Raise(new AssignmentAnswerDeletedEvent(answer));
            return Result.Success();
        }

        var groupId = await _context.AssignmentGroups
            .Where(x => x.RoomId == assignment.RoomId)
            .Where(x => x.AssignmentGroupsStudents.Any(xx => xx.StudentId == userId))
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (groupId == default)
            return AssignmentErrors.UserIsNotInAnyGroup;

        answer = await _context.AssignmentAnswers
            .Where(x => x.AssignmentId == assignmentId && x.AssignmentGroupId == groupId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(answer.File))
            return AssignmentErrors.AnswerNotFound;
        var result1 = await _context.AssignmentAnswers
            .Where(x => x.AssignmentId == assignmentId && x.AssignmentGroupId == groupId)
            .ExecuteDeleteAsync(cancellationToken);
        if (result1 == 0)
            return Error.UnKnown;
        await _fileServices.Delete(answer.File);
        _eventCollection.Raise(new AssignmentAnswerDeletedEvent(answer));
        return Result.Success();
    }
}