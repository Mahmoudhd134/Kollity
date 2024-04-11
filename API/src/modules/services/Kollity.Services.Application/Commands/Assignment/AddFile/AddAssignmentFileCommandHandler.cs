using Kollity.Services.Domain.AssignmentModels;
using Kollity.Services.Application.Abstractions.Files;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.AddFile;

public class AddAssignmentFileCommandHandler : ICommandHandler<AddAssignmentFileCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;

    public AddAssignmentFileCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
    }

    public async Task<Result> Handle(AddAssignmentFileCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
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

        var path = await _fileServices.UploadFile(file, Category.AssignmentFile);
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