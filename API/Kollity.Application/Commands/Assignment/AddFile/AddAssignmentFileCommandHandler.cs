using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.AddFile;

public class AddAssignmentFileCommandHandler : ICommandHandler<AddAssignmentFileCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;
    private readonly IFileAccessor _fileAccessor;

    public AddAssignmentFileCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        IFileAccessor fileAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _fileAccessor = fileAccessor;
    }

    public async Task<Result> Handle(AddAssignmentFileCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userAccessor.GetCurrentUserId(),
            assignmentId = request.Id;
        var file = request.AddAssignmentFileDto.File;

        var assignmentDoctorId = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .Select(x => x.DoctorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (assignmentDoctorId is null || assignmentDoctorId == Guid.Empty)
            return AssignmentErrors.AssignmentHasNoDoctor;

        if (assignmentDoctorId != userId)
            return AssignmentErrors.UnAuthorizedAddFile;

        var path = await _fileAccessor.UploadFile(file);
        _context.AssignmentFiles.Add(new AssignmentFile
        {
            AssignmentId = assignmentId,
            FilePath = path,
            Name = file.FileName,
            UploadDate = DateTime.UtcNow
        });

        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        result = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .ExecuteUpdateAsync(c =>
                c.SetProperty(x => x.LastUpdateDate, DateTime.UtcNow), cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        
        return Result.Success();
    }
}