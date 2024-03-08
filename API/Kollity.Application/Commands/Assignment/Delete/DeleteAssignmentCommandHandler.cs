using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Delete;

public class DeleteAssignmentCommandHandler : ICommandHandler<DeleteAssignmentCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IFileAccessor _fileAccessor;
    private readonly IUserAccessor _userAccessor;

    public DeleteAssignmentCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        IFileAccessor fileAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _fileAccessor = fileAccessor;
=======
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;

    public DeleteAssignmentCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        Guid assignmentId = request.Id,
<<<<<<< HEAD
            userId = _userAccessor.GetCurrentUserId();
=======
            userId = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e

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

<<<<<<< HEAD
        await _fileAccessor.Delete(files);
        await _fileAccessor.Delete(answers);
=======
        await _fileServices.Delete(files);
        await _fileServices.Delete(answers);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e

        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}