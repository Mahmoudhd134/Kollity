using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.DeleteFile;

public class DeleteAssignmentFileCommandHandler : ICommandHandler<DeleteAssignmentFileCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IFileAccessor _fileAccessor;
    private readonly IUserAccessor _userAccessor;

    public DeleteAssignmentFileCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        IFileAccessor fileAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _fileAccessor = fileAccessor;
=======
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;

    public DeleteAssignmentFileCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(DeleteAssignmentFileCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        Guid userId = _userAccessor.GetCurrentUserId(),
=======
        Guid userId = _userServices.GetCurrentUserId(),
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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

<<<<<<< HEAD
        await _fileAccessor.Delete(fileInfo.FilePath);
=======
        await _fileServices.Delete(fileInfo.FilePath);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        result = await _context.Assignments
            .Where(x => x.Id == fileInfo.AssignmentId)
            .ExecuteUpdateAsync(c =>
                c.SetProperty(x => x.LastUpdateDate, DateTime.UtcNow), cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        return Result.Success();
    }
}