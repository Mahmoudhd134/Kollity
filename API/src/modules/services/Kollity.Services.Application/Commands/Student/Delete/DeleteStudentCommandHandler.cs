using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Student;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Student.Delete;

public class DeleteStudentCommandHandler : ICommandHandler<DeleteStudentCommand>
{
    private readonly IFileServices _fileServices;
    private readonly ApplicationDbContext _context;
    private readonly EventCollection _eventCollection;

    public DeleteStudentCommandHandler(IFileServices fileServices, ApplicationDbContext context,
        EventCollection eventCollection)
    {
        _fileServices = fileServices;
        _context = context;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var studentFound = await _context.Students.AnyAsync(x => x.Id == request.Id, cancellationToken);
        if (studentFound == false)
            return StudentErrors.IdNotFound(request.Id);

        var files = await _context.AssignmentAnswers
            .Where(x => x.StudentId == request.Id && x.AssignmentGroupId == null)
            .Select(x => x.File)
            .ToListAsync(cancellationToken);

        await _context.AssignmentAnswerDegrees
            .Where(x => x.StudentId == request.Id)
            .ExecuteDeleteAsync(cancellationToken);

        var result = await _context.Students
            .Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken);

        if (result == 0)
            return Error.UnKnown;
        
        _eventCollection.Raise(new StudentDeletedEvent(request.Id));

        await _fileServices.Delete(files);
        return Result.Success();
    }
}