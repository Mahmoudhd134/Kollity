using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.DeleteFile;

public class DeleteAssignmentFileCommandHandler : ICommandHandler<DeleteAssignmentFileCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;
    private readonly IFileAccessor _fileAccessor;

    public DeleteAssignmentFileCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        IFileAccessor fileAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _fileAccessor = fileAccessor;
    }

    public async Task<Result> Handle(DeleteAssignmentFileCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userAccessor.GetCurrentUserId(),
            fileId = request.Id;

        var fileInfo = await _context.AssignmentFiles
            .Where(x => x.Id == fileId)
            .Select(x => new { x.Assignment.DoctorId, x.FilePath, x.AssignmentId })
            .FirstOrDefaultAsync(cancellationToken);

        if (fileInfo is null)
            return AssignmentErrors.FileNotFound(fileId);
        if (fileInfo.DoctorId == Guid.Empty)
            return AssignmentErrors.AssignmentHasNoDoctor;
        if (fileInfo.DoctorId != userId)
            return AssignmentErrors.UnAuthorizedAddFile;

        var result = await _context.AssignmentFiles
            .Where(x => x.Id == fileId)
            .ExecuteDeleteAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        await _fileAccessor.Delete(fileInfo.FilePath);
        result = await _context.Assignments
            .Where(x => x.Id == fileInfo.AssignmentId)
            .ExecuteUpdateAsync(c =>
                c.SetProperty(x => x.LastUpdateDate, DateTime.UtcNow), cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        return Result.Success();
    }
}