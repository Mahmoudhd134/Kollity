using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Delete;

public class DeleteAssignmentCommandHandler : ICommandHandler<DeleteAssignmentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;

    public DeleteAssignmentCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
    }

    public async Task<Result> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        Guid assignmentId = request.Id,
            userId = _userServices.GetCurrentUserId();

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

        await _fileServices.Delete(files);
        await _fileServices.Delete(answers);

        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}