using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Delete;

public class DeleteAssignmentCommandHandler : ICommandHandler<DeleteAssignmentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;
    private readonly IFileAccessor _fileAccessor;

    public DeleteAssignmentCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        IFileAccessor fileAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _fileAccessor = fileAccessor;
    }

    public async Task<Result> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        Guid assignmentId = request.Id,
            userId = _userAccessor.GetCurrentUserId();

        var assignmentDoctorId = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .Select(x => x.DoctorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (assignmentDoctorId is null)
            return AssignmentErrors.NotFound(assignmentId);
        if (assignmentDoctorId == Guid.Empty)
            return AssignmentErrors.AssignmentHasNoDoctor;

        if (assignmentDoctorId != userId)
            return AssignmentErrors.UnAuthorizedDelete;

        var files = await _context.AssignmentFiles
            .Where(x => x.AssignmentId == assignmentId)
            .Select(x => x.FilePath)
            .ToListAsync(cancellationToken);

        var result = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .ExecuteDeleteAsync(cancellationToken);

        await _fileAccessor.Delete(files);

        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}