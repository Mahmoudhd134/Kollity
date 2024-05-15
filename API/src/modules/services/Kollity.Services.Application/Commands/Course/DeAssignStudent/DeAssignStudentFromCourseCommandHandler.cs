using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.DeAssignStudent;

public class DeAssignStudentFromCourseCommandHandler : ICommandHandler<DeAssignStudentFromCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly EventCollection _eventCollection;

    public DeAssignStudentFromCourseCommandHandler(ApplicationDbContext context, EventCollection eventCollection)
    {
        _context = context;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeAssignStudentFromCourseCommand request, CancellationToken cancellationToken)
    {
        Guid sid = request.Ids.StudentId,
            cid = request.Ids.CourseId;

        var studentCourse = await _context.StudentCourses
            .Where(x => x.StudentId == sid && x.CourseId == cid)
            .FirstOrDefaultAsync(cancellationToken);
        if (studentCourse is null)
            return CourseErrors.StudentIsNotAssignedToThisCourse;

        _context.StudentCourses.Remove(studentCourse);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new CourseStudentDeAssignedEvent(studentCourse));
        return Result.Success();
    }
}