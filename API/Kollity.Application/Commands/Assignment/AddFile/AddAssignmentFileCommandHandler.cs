using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.AssignmentModels;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.AddFile;

public class AddAssignmentFileCommandHandler : ICommandHandler<AddAssignmentFileCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IFileAccessor _fileAccessor;
    private readonly IUserAccessor _userAccessor;

    public AddAssignmentFileCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        IFileAccessor fileAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _fileAccessor = fileAccessor;
=======
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;

    public AddAssignmentFileCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(AddAssignmentFileCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        Guid userId = _userAccessor.GetCurrentUserId(),
=======
        Guid userId = _userServices.GetCurrentUserId(),
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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

<<<<<<< HEAD
        var path = await _fileAccessor.UploadFile(file, Category.AssignmentFile);
=======
        var path = await _fileServices.UploadFile(file, Category.AssignmentFile);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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