using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Assignment;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Delete;

public class DeleteAssignmentCommandHandler : ICommandHandler<DeleteAssignmentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileServices _fileServices;
    private readonly EventCollection _eventCollection;
    private readonly IUserServices _userServices;

    public DeleteAssignmentCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices, EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        Guid assignmentId = request.Id,
            userId = _userServices.GetCurrentUserId();

        var assignment = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (assignment is null)
            return AssignmentErrors.NotFound(assignmentId);
        if (assignment.DoctorId == Guid.Empty)
            return AssignmentErrors.AssignmentHasNoDoctor;

        if (assignment.DoctorId != userId)
            return AssignmentErrors.UnAuthorizedDelete;

        var files = await _context.AssignmentFiles
            .Where(x => x.AssignmentId == assignmentId)
            .Select(x => x.FilePath)
            .ToListAsync(cancellationToken);

        var answers = await _context.AssignmentAnswers
            .Where(x => x.AssignmentId == assignmentId)
            .Select(x => x.File)
            .ToListAsync(cancellationToken);

        await _context.AssignmentAnswerDegrees
            .Where(x => x.AssignmentId == assignmentId)
            .ExecuteDeleteAsync(cancellationToken);

        var result = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .ExecuteDeleteAsync(cancellationToken);

        if (result == 0)
            return Error.UnKnown;

        await _fileServices.Delete(files.Concat(answers).ToList());

        _eventCollection.Raise(new AssignmentDeletedEvent(assignment));

        return Result.Success();
    }
}