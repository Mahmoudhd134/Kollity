using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.Delete;

public class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileServices _fileServices;
    private readonly EventCollection _eventCollection;

    public DeleteCourseCommandHandler(ApplicationDbContext context, IFileServices fileServices,
        EventCollection eventCollection)
    {
        _context = context;
        _fileServices = fileServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var courseId = request.Id;
        var contents = await _context.Rooms
            .Where(x => x.CourseId == courseId)
            .Select(x => new
            {
                Contents = x.RoomContents.Select(xx => xx.FilePath).ToList(),
                AssingmentFiles = x.Assignments.SelectMany(xx =>
                    xx.AssignmentFiles.Select(xxx => xxx.FilePath)).ToList(),
                AssignmentAnswers = x.Assignments.SelectMany(xx =>
                    xx.AssignmentsAnswers.Select(xxx => xxx.File)).ToList()
            })
            .ToListAsync(cancellationToken);

        var course = await _context.Courses
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (course is null)
            return CourseErrors.IdNotFound(courseId);

        _context.Courses.Remove(course);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new CourseDeletedEvent(course));

        foreach (var content in contents)
        {
            await _fileServices.Delete(content.Contents);
            await _fileServices.Delete(content.AssingmentFiles);
            await _fileServices.Delete(content.AssignmentAnswers);
        }

        return Result.Success();
    }
}