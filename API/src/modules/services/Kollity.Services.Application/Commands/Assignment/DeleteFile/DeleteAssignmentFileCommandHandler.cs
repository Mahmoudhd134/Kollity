using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.DeleteFile;

public class DeleteAssignmentFileCommandHandler : ICommandHandler<DeleteAssignmentFileCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;

    public DeleteAssignmentFileCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
    }

    public async Task<Result> Handle(DeleteAssignmentFileCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
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
            return AssignmentErrors.UnAuthorizedDeleteFile;

        var result = await _context.AssignmentFiles
            .Where(x => x.Id == fileId)
            .ExecuteDeleteAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        await _fileServices.Delete(fileInfo.FilePath);
        result = await _context.Assignments
            .Where(x => x.Id == fileInfo.AssignmentId)
            .ExecuteUpdateAsync(c =>
                c.SetProperty(x => x.LastUpdateDate, DateTime.UtcNow), cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        return Result.Success();
    }
}