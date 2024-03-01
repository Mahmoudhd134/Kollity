using Kollity.Application.Abstractions.Files;
using Kollity.Application.Extensions;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Student.Delete;

public class DeleteStudentCommandHandler : ICommandHandler<DeleteStudentCommand>
{
    private readonly UserManager<Domain.StudentModels.Student> _studentManager;
    private readonly IFileAccessor _fileAccessor;
    private readonly ApplicationDbContext _context;

    public DeleteStudentCommandHandler(UserManager<Domain.StudentModels.Student> studentManager,
        IFileAccessor fileAccessor, ApplicationDbContext context)
    {
        _studentManager = studentManager;
        _fileAccessor = fileAccessor;
        _context = context;
    }

    public async Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentManager.FindByIdAsync(request.Id.ToString());

        if (student is null)
            return StudentErrors.IdNotFound(request.Id);

        var files = await _context.AssignmentAnswers
            .Where(x => x.StudentId == request.Id && x.AssignmentGroupId == null)
            .Select(x => x.File)
            .ToListAsync(cancellationToken);

        await _context.AssignmentAnswerDegrees
            .Where(x => x.StudentId == student.Id)
            .ExecuteDeleteAsync(cancellationToken);
        
        var result = await _studentManager.DeleteAsync(student);
        if (result.Succeeded == false) 
            return result.Errors.ToAppError().ToList();

        await _fileAccessor.Delete(files);
        return Result.Success();
    }
}